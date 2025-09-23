using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    [SerializeField] private TMP_Text attackText;

    public List<EnemyAttackEffect> CurrentAttacks { get; private set; } = new List<EnemyAttackEffect>();
    public EnemyAttackEffect NextAttack { get; private set; }

    public void Setup(EnemyData enemyData)
    {
        CurrentAttacks = new List<EnemyAttackEffect>(enemyData.Attacks);
        
        ChooseNextAttack();
        UpdateAttackText();
        SetupBase(enemyData.Health, enemyData.Armour, enemyData.Image);
    }

    public void UpdateAttackText()
    {
        if (NextAttack != null)
        {
            attackText.text = $"{NextAttack.AttackDescription}: {NextAttack.attackEffect.GetValue()}";
        }
    }

    public void UpdateAttackText(int damage)
    {
        //if (NextAttack != null)
        //{
        //    attackText.text = $"{NextAttack.GetAttackName()}: {damage}";
        //}
        //else
        //{
        //    attackText.text = $"ATK: {AttackPower}";
        //}
        attackText.text = $"{NextAttack.AttackDescription}: {damage}";
        Debug.Log(damage);
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
