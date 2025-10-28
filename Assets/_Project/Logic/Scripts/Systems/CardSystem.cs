using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;


    private readonly List<Card> _drawPile = new();
    private readonly List<Card> _discardPile = new();
    private readonly List<Card> _hand = new();

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<StartBattleGA>(StartBattlePreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<StartBattleGA>(StartBattlePostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<StartBattleGA>(StartBattlePreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<StartBattleGA>(StartBattlePostReaction, ReactionTiming.POST);
    }

    //Setup

    public void Init(List<CardData> deckData)
    {
        foreach(var cardData in deckData)
        {
            Card card = new(cardData);
            _drawPile.Add(card);
        }
    }

    //Performers

    private IEnumerator DrawCardPerformer(DrawCardsGA drawCardsGA)
    {
        int actualAmount = Mathf.Min(drawCardsGA.Amount, _drawPile.Count);
        int notDrawnAmount = drawCardsGA.Amount - actualAmount;

        for(int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }

        if(notDrawnAmount > 0)
        {
            RefillDeck();

            for(int i = 0;i < notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }

    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach(var card in _hand)
        {
            CardView cardView = handView.RemoveCard(card);
            AudioSystem.Instance.PlaySFX("SFX_DISCARD_CARD");
            yield return DiscardCard(cardView);
        }
        _hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        _hand.Remove(playCardGA.Card);
        CardView cardView = handView.RemoveCard(playCardGA.Card);
        yield return DiscardCard(cardView);

        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);

        if(playCardGA.Card.ManualTargetEffect != null)
        {
            EnemyView target = playCardGA.ManualTarget;
            PerformEffectGA performEffectGA = new(playCardGA.Card.ManualTargetEffect, target);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }

        foreach(var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
    }

    //Reactions

    private void StartBattlePreReaction(StartBattleGA startBattleGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.Perform(discardAllCardsGA);

        Debug.Log("Card System: On Start Battle ");
    }

    private void StartBattlePostReaction(StartBattleGA startBattleGA)
    {
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }

    //Helpers

    private IEnumerator DrawCard()
    {
        Card card = _drawPile.Draw();
        _hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        AudioSystem.Instance.PlaySFX("SFX_DRAW_CARD");
        yield return handView.AddCard(cardView);
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        _discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.transform.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }

    private void RefillDeck()
    {
        _drawPile.AddRange(_discardPile);
        _discardPile.Clear();
    }
}
