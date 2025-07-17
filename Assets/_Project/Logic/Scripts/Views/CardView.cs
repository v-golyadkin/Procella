using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _mana;
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private GameObject _wrapper;
    [SerializeField] private LayerMask _dropAreaLayer;

    public Card Card { get; private set; }

    private Vector3 _dragStartPosition;
    private Quaternion _dragStartRotation;

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
        if(!Interactions.Instance.PlayerCanHover()) return;

        _wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2f, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    private void OnMouseExit()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;

        CardViewHoverSystem.Instance.Hide();
        _wrapper.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;

        Interactions.Instance.PlayerIsDraging = true;
        _wrapper.SetActive(true);
        CardViewHoverSystem.Instance.Hide();
        _dragStartPosition = transform.position;
        _dragStartRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseDrag()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;

        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseUp()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;

        if( ManaSystem.Instance.HasEnoughMana(Card.Mana)
            && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, _dropAreaLayer))
        {
            PlayCardGA playCardGA = new(Card);
            ActionSystem.Instance.Perform(playCardGA);
        }
        else
        {
            transform.position = _dragStartPosition;
            transform.rotation = _dragStartRotation;
        }

        Interactions.Instance.PlayerIsDraging = false;
    }
}
