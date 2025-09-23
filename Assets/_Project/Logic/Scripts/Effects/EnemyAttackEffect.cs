using SerializeReferenceEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyAttackEffect
{
    [field: SerializeField] public string AttackDescription;
    [field: SerializeReference, SR] public Effect attackEffect { get; private set; }
    //[SerializeField] private List<AutoTargetEffect> effects = new List<AutoTargetEffect>();
    [SerializeField] private bool isBuff;

    public void PerformAttack(EnemyView enemy)
    {
        AttackHeroGA attackHeroGA = new AttackHeroGA(enemy, attackEffect, isBuff);
        ActionSystem.Instance.AddReaction(attackHeroGA);
    }
}