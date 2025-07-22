using System.Collections.Generic;
using UnityEngine;

public class Perk
{
    public Sprite Image => _data.Image; 

    private readonly PerkData _data;
    private readonly PerkCondition _condition;
    private readonly AutoTargetEffect _effect;

    public Perk(PerkData data)
    {
        _data = data;
        _condition = _data.PerkCondition;
        _effect = _data.AutoTargetEffect;
    }

    public void OnAdd()
    {
        _condition.SubscribeCondition(Reaction);
    }

    public void OnRemove()
    {
        _condition.UnsubscribeCondition(Reaction);
    }

    private void Reaction(GameAction gameAction)
    {
        if (_condition.SubConditionIsMet(gameAction))
        {
            List<CombatantView> targets = new();
            if (_data.UseActionCasterAsTarget && gameAction is IHaveCaster haveCaster)
            {
                targets.Add(haveCaster.Caster);
            }
            if (_data.UseAutoTarget)
            {
                targets.AddRange(_effect.TargetMode.GetTargets());
            }
            GameAction perkEffectAction = _effect.Effect.GetGameAction(targets, HeroSystem.Instance.HeroView);
            ActionSystem.Instance.AddReaction(perkEffectAction);
        }
    }
}
