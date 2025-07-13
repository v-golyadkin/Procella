using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField] private CardView _cardViewHover;

    public void Show(Card card, Vector3 position)
    {
        _cardViewHover.gameObject.SetActive(true);
        _cardViewHover.Setup(card);
        _cardViewHover.transform.position = position;
    }

    public void Hide()
    {
        _cardViewHover.gameObject.SetActive(false);
    }
}
