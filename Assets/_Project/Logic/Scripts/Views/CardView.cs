using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private SpriteRenderer image;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropAreaLayer;

    public Card Card { get; private set; }

    private Vector3 _dragStartPosition;
    private Quaternion _dragStartRotation;

    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        image.sprite = card.Image;
    }

    private void OnMouseEnter()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;

        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2f, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    private void OnMouseExit()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;

        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

    private void OnMouseDown()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;

        if(Card.ManualTargetEffect != null)
        {
            ManualTargetSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            Interactions.Instance.PlayerIsDraging = true;
            wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();
            _dragStartPosition = transform.position;
            _dragStartRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }
    }

    private void OnMouseDrag()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;
        
        if (Card.ManualTargetEffect != null) return;

        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseUp()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;

        if(Card.ManualTargetEffect != null)
        {
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(-1));
            if(target != null && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
        }
        else
        {
            if (ManaSystem.Instance.HasEnoughMana(Card.Mana)
                && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropAreaLayer))
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
}
