using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardView enemyBoardView;

    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;
    
    private List<EnemyView> _enemiesBeforeStatusEffects;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
        //ActionSystem.AttachPerformer<PlayerTurnEndGA>(PlayerTurnEndPerformer);
        ActionSystem.SubscribeReaction<PlayerTurnEndGA>(PlayerTurnEndPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<PlayerTurnEndGA>(PlayerTurnEndPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<AttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
        //ActionSystem.DetachPerformer<PlayerTurnEndGA>();
        ActionSystem.UnsubscribeReaction<PlayerTurnEndGA>(PlayerTurnEndPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<PlayerTurnEndGA>(PlayerTurnEndPostReaction, ReactionTiming.POST);
    }

    public void Init(List<EnemyData> enemyDatas)
    {
        SpawnEnemy(enemyDatas);
    }

    public void SpawnEnemy(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }

    public void KillAllEnemies()
    {
        StartCoroutine(KillAllEnemiesCoroutine());
    }

    private IEnumerator KillAllEnemiesCoroutine()
    {
        var enemiesToKill = new List<EnemyView>(enemyBoardView.EnemyViews);

        foreach (var enemyView in enemiesToKill)
        {
            KillEnemyGA killEnemyGA = new KillEnemyGA(enemyView);

            bool performed = true;
            ActionSystem.Instance.Perform(killEnemyGA, () =>
            {
                performed = false;
            });

            yield return new WaitUntil(() => performed);

            yield return null;
        }
    }

    //Performers

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        foreach (var enemy in enemyBoardView.EnemyViews)
        {
            enemy.PerformNextAttack();
        }

        yield return null;
    }

    private IEnumerator AttackHeroPerformer(AttackHeroGA attackHeroGA)
    {
        EnemyView attacker = attackHeroGA.Attacker;
        if (attackHeroGA.Attacker != null)
        {
            //DealDamageGA dealDamageGA = new(attackHeroGA.Damage, new() { HeroSystem.Instance.HeroView }, attackHeroGA.Caster);
            //ActionSystem.Instance.AddReaction(dealDamageGA);
            if (attackHeroGA.IsBuff)
            {
                Tween tween = attacker.transform.DOMoveY(attacker.transform.position.y + 0.15f, 0.15f);
                yield return tween.WaitForCompletion();
                attacker.transform.DOMoveY(attacker.transform.position.y - 0.15f, 0.15f);
                PerformEffectGA performEffectGA = new(attackHeroGA.Effect, attackHeroGA.Attacker);
                ActionSystem.Instance.AddReaction(performEffectGA);
            }
            else
            {
                Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
                yield return tween.WaitForCompletion();
                attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
                PerformEffectGA performEffectGA = new(attackHeroGA.Effect, HeroSystem.Instance.HeroView);
                ActionSystem.Instance.AddReaction(performEffectGA);
            }
            attacker.UpdateAttackText();
        }
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }

    //Reaction

    private void PlayerTurnEndPreReaction(PlayerTurnEndGA playerTurnGA)
    {
        Debug.Log("EnemySystem: Applying status effects (PRE)");

        _enemiesBeforeStatusEffects = new List<EnemyView>(enemyBoardView.EnemyViews);

        foreach (var enemy in _enemiesBeforeStatusEffects)
        {
            int burnStacks = enemy.GetStatusEffectStacks(StatusEffectType.BURN);
            int poisonStacks = enemy.GetStatusEffectStacks(StatusEffectType.POISON);

            if (burnStacks > 0)
            {
                ApplyBurnGA applyBurnGA = new(burnStacks, enemy);
                ActionSystem.Instance.AddReaction(applyBurnGA);
            }
            if (poisonStacks > 0)
            {
                ApplyPoisonGA applyPoisonGA = new(poisonStacks, enemy);
                ActionSystem.Instance.AddReaction(applyPoisonGA);
            }
        }
    }
    private void PlayerTurnEndPostReaction(PlayerTurnEndGA playerTurnGA)
    {
        if (_enemiesBeforeStatusEffects != null &&
            _enemiesBeforeStatusEffects.Count > 0 &&
            enemyBoardView.EnemyViews.Count > 0)
        {
            Debug.Log("EnemySystem: Starting enemy turn (POST)");
            EnemyTurnGA enemyTurnGA = new();
            ActionSystem.Instance.Perform(enemyTurnGA);
        }

        _enemiesBeforeStatusEffects = null;
    }
}
