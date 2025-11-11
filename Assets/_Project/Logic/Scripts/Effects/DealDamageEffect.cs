using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int damageAmount;
    [SerializeField] private bool ignoredArmour;

    private CombatantView _currentCaster;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        _currentCaster = caster;
        int totalDamage = damageAmount;

        totalDamage += caster.DamageModifier;

        DealDamageGA dealDamageGA = new(totalDamage, targets, caster, ignoredArmour);
        return dealDamageGA;
    }

    public override int GetValue() => damageAmount;
}
