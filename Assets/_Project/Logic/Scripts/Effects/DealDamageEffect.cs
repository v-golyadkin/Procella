using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    [SerializeField] private bool ignoredArmour;

    public override GameAction GetGameAction(List<CombatantView> targets,CombatantView caster)
    {
        DealDamageGA dealDamageGA = new(damageAmount, targets, caster, ignoredArmour);
        return dealDamageGA;
    }

    public override int GetValue() => damageAmount;
}
