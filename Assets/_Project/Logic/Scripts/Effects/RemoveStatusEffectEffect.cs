using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffectEffect : Effect
{
    [SerializeField] private StatusEffectType statusEffectType;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        return new RemoveStatusEffectGA(statusEffectType, targets);
    }

    public override int GetValue()
    {
        throw new System.NotImplementedException();
    }
}
