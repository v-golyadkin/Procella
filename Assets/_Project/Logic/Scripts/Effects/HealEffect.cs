using System.Collections.Generic;
using UnityEngine;

public class HealEffect : Effect
{
    [SerializeField] private int _healAmount;
    public override GameAction GetGameAction(List<CombatantView> targets, CombatantView caster)
    {
        HealGA healGA = new(_healAmount, targets, caster);
        return healGA;
    }
}
