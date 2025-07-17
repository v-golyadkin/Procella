using TMPro;
using UnityEngine;

public class ManaUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _mana;

    public void UpdateManaText(int currentMana)
    {
        _mana.text = currentMana.ToString();
    }
}
