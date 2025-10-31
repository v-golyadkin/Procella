using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction, IHaveCaster
{
    public int Damage { get; set; }
    public List<CombatantView> Targets { get; set; }
    public CombatantView Caster { get; private set; }
    public bool IgnoredArmour { get; private set; }
    public DealDamageGA(int damage, List<CombatantView> targets, CombatantView caster, bool ignoredArmour = false)
    {
        Damage = damage;
        Targets = new(targets);
        Caster = caster;
        IgnoredArmour = ignoredArmour;

        Debug.Log($"{caster.gameObject.name} deals {damage} + {caster.DamageModifier} = {Damage} damage");
    }
}
