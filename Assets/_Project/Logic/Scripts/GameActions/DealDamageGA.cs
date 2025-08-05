using System.Collections.Generic;

public class DealDamageGA : GameAction, IHaveCaster
{
    public int Amount { get; set; }
    public List<CombatantView> Targets { get; set; }
    public CombatantView Caster { get; private set; }
    public bool IgnoredArmour { get; private set;}
    public DealDamageGA(int amount, List<CombatantView> targets, CombatantView caster, bool ignoredArmour = false)
    {
        Amount = amount;
        Targets = new(targets);
        Caster = caster;
        IgnoredArmour = ignoredArmour;
    }
}
