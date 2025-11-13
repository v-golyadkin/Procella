using System;
using UnityEngine;

public class OnHealthThresholdCondition : PerkCondition
{
    [SerializeField] private float healthPercent = 50f;
    [SerializeField] private bool whenBelow = true;
    [SerializeField] private bool triggerOnce = false;
    [SerializeField] private bool isHeroThreshold = true; // true - hero, false - enemy

    private bool _alreadyTriggered;
    private float _lastHealthPercent;

    public override bool SubConditionIsMet(GameAction gameAction)
    {
        if(triggerOnce && _alreadyTriggered)
        {
            return false;
        }

        var target = GetTarget(gameAction);
        if(target == null)
        {
            return false;       
        }


        float currentHealthPercent = (float)target.CurrentHelth / target.MaxHealth * 100f;

        if(_lastHealthPercent < 0)
        {
            _lastHealthPercent = currentHealthPercent;
            return false;
        }

        bool crossedThreshold = CheckThresholdCrossing(currentHealthPercent, _lastHealthPercent);

        if (crossedThreshold && triggerOnce)
        {
            _alreadyTriggered = true;
        }

        //Debug.Log($"Health: {currentHealthPercent}%, Last: {_lastHealthPercent}%, Crossed: {crossedThreshold}");

        return crossedThreshold;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ResetState();

        ActionSystem.SubscribeReaction<DealDamageGA>(reaction, timing);
        ActionSystem.SubscribeReaction<HealGA>(reaction, timing);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ResetState();

        ActionSystem.UnsubscribeReaction<DealDamageGA>(reaction, timing);
        ActionSystem.UnsubscribeReaction<HealGA>(reaction, timing);
    }

    private CombatantView GetTarget(GameAction gameAction)
    {
        if (isHeroThreshold)
        {
            return HeroSystem.Instance.HeroView;
        }
        else
        {
            if(gameAction is DealDamageGA damageGA && damageGA.Targets.Count > 0)
            {
                return damageGA.Targets[0];
            }
            if(gameAction is HealGA healGA && healGA.Targets.Count > 0)
            {
                return healGA.Targets[0];
            }
        }
        return null;
    }

    private bool CheckThresholdCrossing(float currentPercent, float lastPercent)
    {
        if (whenBelow)
        {
            return lastPercent > healthPercent && currentPercent <= healthPercent;
        }
        else
        {
            return lastPercent < healthPercent && currentPercent >= healthPercent;
        }
    }

    private void ResetState()
    {
        _alreadyTriggered = false;
        _lastHealthPercent = -1f;
    }
}
