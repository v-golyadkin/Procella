using UnityEngine;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private HandView _handView;

    [SerializeField] private CardData _cardData;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card card = new(_cardData);
            CardView cardView = CardViewCreator.Instance.CreateCardView(card, transform.position, Quaternion.identity);
            StartCoroutine(_handView.AddCard(cardView));
        }
    }
}
