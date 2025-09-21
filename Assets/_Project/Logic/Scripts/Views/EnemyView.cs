using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;

    public int AttackPower {  get; set; }

    public List<EnemyAttack> CurrentAttacks { get; private set; } = new List<EnemyAttack>();
    public EnemyAttack NextAttack { get; private set; }

    public void Setup(EnemyData enemyData)
    {
        AttackPower = enemyData.AttackPower;
        CurrentAttacks = new List<EnemyAttack>(enemyData.Attacks);
        
        ChooseNextAttack();
        UpdateAttackText();
        SetupBase(enemyData.Health, enemyData.Armour, enemyData.Image);
    }

    public void UpdateAttackText()
    {
        if (NextAttack != null)
        {
            attackText.text = $"{NextAttack.GetAttackName()}: {NextAttack.GetDamage()}";
        }
        else
        {
            attackText.text = $"ATK: {AttackPower}";
        }

    }

    public void ChooseNextAttack()
    {
        if(CurrentAttacks.Count > 0)
        {
            NextAttack = CurrentAttacks[Random.Range(0, CurrentAttacks.Count)];
        }
        else
        {
            NextAttack = null;
        }
    }

    public void PerformNextAttack()
    {
        if(NextAttack != null)
        {
            NextAttack.PerformAttack(this);

            ChooseNextAttack();
        }
        else
        {
            AttackHeroGA attackHeroGA = new AttackHeroGA(this);
            ActionSystem.Instance.AddReaction(attackHeroGA);
        }
    }
}
