using System.Collections;
using UnityEngine;

public abstract class EnemyAttack : ScriptableObject
{
    public abstract void PerformAttack(EnemyView enemy);
    public abstract int GetDamage();
    public abstract string GetAttackName();
}
