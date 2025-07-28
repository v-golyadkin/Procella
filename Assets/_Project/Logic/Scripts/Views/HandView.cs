using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;

    private readonly List<CardView> _cards = new();

    public IEnumerator AddCard(CardView card)
    {
        _cards.Add(card);
        yield return UpdateCardPosition(0.15f);
    }

    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if(cardView == null) return null;
        _cards.Remove(cardView);
        StartCoroutine(UpdateCardPosition(0.15f));
        return cardView;
    }

    private CardView GetCardView(Card card)
    {
        return _cards.Where(cardView => cardView.Card == card).FirstOrDefault();
    }

    private IEnumerator UpdateCardPosition(float duration)
    {
        if(_cards.Count == 0) yield break;

        float cardSpacing = 1f / 10;
        float firstCardPosition = 0.5f - (_cards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;
        for(int i = 0; i < _cards.Count; i++)
        {
            float pos = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(pos);
            Vector3 forward = spline.EvaluateTangent(pos);
            Vector3 up = spline.EvaluateUpVector(pos);
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);

            _cards[i].transform.DOMove(splinePosition + transform.position + 0.01f * i * Vector3.back, duration);
            _cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }
}
