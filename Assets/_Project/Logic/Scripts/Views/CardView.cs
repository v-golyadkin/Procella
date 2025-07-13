using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _mana;
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private GameObject _wrapper;

    public Card Card { get; private set; }

    public void Setup(Card card)
    {
        Card = card;
        _title.text = card.Title;
        _description.text = card.Description;
        _mana.text = card.Mana.ToString();
        _image.sprite = card.Image;
    }

    private void OnMouseEnter()
    {
        _wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2f, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    private void OnMouseExit()
    {
        CardViewHoverSystem.Instance.Hide();
        _wrapper.SetActive(true);
    }
}
