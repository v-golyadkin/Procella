public class AttackHeroGA : GameAction, IHaveCaster
{
    public EnemyView Attacker {  get; private set; }

    public CombatantView Caster {  get; private set; }

    public int Damage { get; private set; }

    public AttackHeroGA(EnemyView attacker)
    {
        Attacker = attacker;
        Caster = Attacker;
    }

    public AttackHeroGA(EnemyView attacker, int damage)
    {
        Attacker = attacker;
        Caster = Attacker;
        Damage = damage;
    }
}
