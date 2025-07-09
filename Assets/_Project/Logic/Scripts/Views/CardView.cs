using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _mana;
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private GameObject _wrapper;
}
