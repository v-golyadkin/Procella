using System;
using UnityEngine;

public class OnHealthThresholdCondition : PerkCondition
{
    [SerializeField] private float healthPercent = 50f;
    [SerializeField] private bool whenBelow = true;
    [SerializeField] private bool triggerOnce = false;
    [SerializeField] private bool isHeroThreshold = true; // true - hero, false - enemy

    private bool _alreadyTriggered = false;

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

        float currentPercent = target.CurrentHelth / target.MaxHealth * 100f;

        bool conditionMet = whenBelow ? currentPercent <= healthPercent : currentPercent >= healthPercent;

        if(conditionMet && triggerOnce)
        {
            _alreadyTriggered = true;
        }

        return conditionMet;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<DealDamageGA>(reaction, timing);
        ActionSystem.SubscribeReaction<HealGA>(reaction, timing);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
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
}
