using DG.Tweening;
using UnityEngine;

public class CardViewCreator : Singleton<CardViewCreator>
{
    [SerializeField] private CardView _cardViewPrefab;

    public CardView CreateCardView(Vector3 position, Quaternion rotate)
    {
        CardView cardView = Instantiate(_cardViewPrefab, position, rotate);
        cardView.transform.localScale = Vector3.zero;
        cardView.transform.DOScale(Vector3.one, 0.15f);
        return cardView;
    }
}
