using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyAttacks/BasicAttack", fileName = "BasicAttack")]
public class BasicAttack : EnemyAttack
{
    [SerializeField] private int damage;
    [SerializeField] private string attackName = "Basic Attack";

    public override string GetAttackName() => attackName;
    public override int GetDamage() => damage;

    public override void PerformAttack(EnemyView enemy)
    {
        AttackHeroGA attackHeroGA = new AttackHeroGA(enemy, damage);
        ActionSystem.Instance.AddReaction(attackHeroGA);
    }
}
