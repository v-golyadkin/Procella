using System.Collections.Generic;

public class PerformEffectGA : GameAction
{
    public Effect Effect {  get; set; } 
    public List<CombatantView> Targets { get; set; }
    public PerformEffectGA(Effect effect, List<CombatantView> targets)
    {
        Effect = effect;
        Targets = targets == null ? null : new(targets);
    }

    public PerformEffectGA(Effect effect, CombatantView target)
    {
        Effect = effect;
        Targets = target == null ? null : new List<CombatantView> { target };
    }
}
