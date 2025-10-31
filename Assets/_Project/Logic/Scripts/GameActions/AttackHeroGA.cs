using UnityEngine;

public class AttackHeroGA : GameAction, IHaveCaster
{
    public EnemyView Attacker {  get; private set; }

    public CombatantView Caster {  get; private set; }

    public int Damage { get; private set; }

    public Effect Effect { get; private set; }

    public bool IsBuff { get; private set; }

    public bool IsDamageAttack => Damage > 0 || IsEffectDealingDamage(Effect);

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

    public AttackHeroGA(EnemyView attacker, Effect attackEffect, int damage)
    {
        Attacker = attacker;
        Caster = Attacker;
        Effect = attackEffect;
        Damage = damage;
        IsBuff = false;
    }

    private bool IsEffectDealingDamage(Effect effect)
    {
        if (effect == null) return false;
        return effect is DealDamageEffect;
    }
}
