using System.Collections.Generic;

public class AllEnemiesAndHeroTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        List<CombatantView> targets = new(EnemySystem.Instance.Enemies)
        {
            HeroSystem.Instance.HeroView
        };
        return targets;
    }
}
