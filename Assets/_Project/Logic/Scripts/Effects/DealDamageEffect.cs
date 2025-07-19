using System.Collections.Generic;
using UnityEngine;

public class DealDamageEffect : Effect
{
    [SerializeField] private int _damageAmount;

    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        DealDamageGA dealDamageGA = new(_damageAmount, targets);
        return dealDamageGA;
    }
}
