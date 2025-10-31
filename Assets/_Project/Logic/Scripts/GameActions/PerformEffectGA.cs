using System.Collections.Generic;

public class PerformEffectGA : GameAction
{
    public Effect Effect {  get; set; } 
    public List<CombatantView> Targets { get; set; }
    public CombatantView Caster { get; set; }
    public PerformEffectGA(Effect effect, List<CombatantView> targets, CombatantView caster = null)
    {
        Effect = effect;
        Targets = targets == null ? null : new(targets);
        Caster = caster;
    }

    public PerformEffectGA(Effect effect, CombatantView target, CombatantView caster = null)
    {
        Effect = effect;
        Targets = target == null ? null : new List<CombatantView> { target };
        Caster = caster;
    }
}
