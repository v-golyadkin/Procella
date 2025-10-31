using System.Collections;
using UnityEngine;

public class HeroSystem : Singleton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView {  get; private set; }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<KillPlayerGA>(KillPlayerPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<KillPlayerGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Init(HeroData heroData)
    {
        HeroView heroView = HeroViewCreator.Instance.CreateHeroView(heroData);
        HeroView = heroView;
    }

    //Performers

    private IEnumerator KillPlayerPerformer(KillPlayerGA killPlayerGA)
    {
        Debug.Log("Player died");

        yield return null;
    }

    //Reactions

    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        int burnStacks = HeroView.GetStatusEffectStacks(StatusEffectType.BURN);
        int poisonStacks = HeroView.GetStatusEffectStacks(StatusEffectType.POISON);
        if(burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }
        if(poisonStacks > 0)
        {
            ApplyPoisonGA applyPoisonGA = new(poisonStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyPoisonGA);
        }
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }
}
