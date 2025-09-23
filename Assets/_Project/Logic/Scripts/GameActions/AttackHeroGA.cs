public class AttackHeroGA : GameAction, IHaveCaster
{
    public EnemyView Attacker {  get; private set; }

    public CombatantView Caster {  get; private set; }

    public int Damage { get; private set; }

    public Effect Effect { get; private set; }

    public bool IsBuff { get; private set; }

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

    public AttackHeroGA(EnemyView attacker, Effect attackEffect, bool isBuff = false)
    {
        Attacker = attacker;
        Caster = Attacker;
        Effect = attackEffect;
        IsBuff = isBuff;
    }
}
