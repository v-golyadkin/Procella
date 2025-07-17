using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public int MaxHealth {  get; private set; }
    public int CurrentHelth { get; private set; }

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHelth = health;
        _spriteRenderer.sprite = image;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        _healthText.text = $"HP: {CurrentHelth}";
    }
}
