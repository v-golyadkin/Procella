using System.Collections.Generic;
using UnityEngine;

public class RefillManaEffect : Effect
{
    [SerializeField] private int refillManaAmount;

    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        RefillManaGA refillManaGA = new(refillManaAmount);
        return refillManaGA;
    }

    public override int GetValue() => refillManaAmount;
}
