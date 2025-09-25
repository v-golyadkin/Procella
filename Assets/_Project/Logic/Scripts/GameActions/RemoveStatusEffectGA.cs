using System.Collections.Generic;
using UnityEngine;

public class RemoveStatusEffectGA : GameAction
{
    public StatusEffectType StatusEffectsType { get; private set; }

    public List<CombatantView> Targets { get; private set; }

    public RemoveStatusEffectGA(StatusEffectType statusEffectsType, List<CombatantView> targets)
    {
        StatusEffectsType = statusEffectsType;
        Targets = targets;
    }
}