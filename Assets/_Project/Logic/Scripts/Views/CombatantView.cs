using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour, IHaveDamageModifier
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private StatusEffectsUI statusEffectsUI;

    public int MaxHealth {  get; private set; }
    public int CurrentHelth { get; private set; }

    public int DamageModifier { get; protected set; }

    private Dictionary<StatusEffectType, int> _statusEffects = new();

    protected void SetupBase(int health, int armour, Sprite image)
    {
        MaxHealth = CurrentHelth = health;
        spriteRenderer.sprite = image;
        DamageModifier = 0;
        AddStatusEffect(StatusEffectType.ARMOR, armour);
        UpdateHealthText();   
    }

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHelth = health;
        spriteRenderer.sprite = image;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"HP: {CurrentHelth}";
    }

    public void Damage(int damageAmount, bool ignoredArmour)
    {
        int remainingDamage = damageAmount;
        int currentArmour = GetStatusEffectStacks(StatusEffectType.ARMOR);

        if(currentArmour > 0 && !ignoredArmour)
        {
            if(currentArmour >= damageAmount)
            {
                RemoveStatusEffect(StatusEffectType.ARMOR, remainingDamage);
                remainingDamage = 0;
            }
            else if(currentArmour < damageAmount) 
            {
                RemoveStatusEffect(StatusEffectType.ARMOR, currentArmour);
                remainingDamage -= currentArmour;
            }
        }

        if(remainingDamage > 0)
        {
            CurrentHelth -= remainingDamage;
            if (CurrentHelth < 0)
            {
                CurrentHelth = 0;
            }
        }

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }

    public void Heal(int healAmount)
    {
        CurrentHelth += healAmount;
        if(CurrentHelth > MaxHealth)
        {
            CurrentHelth = MaxHealth;
        }

        UpdateHealthText();
    }

    public int GetStatusEffectStacks(StatusEffectType type)
    {
        if(_statusEffects.ContainsKey(type)) return _statusEffects[type];
        else return 0;
    }

    public void AddStatusEffect(StatusEffectType type, int stackCount)
    {
        if (_statusEffects.ContainsKey(type))
        {
            _statusEffects[type] += stackCount;
        }
        else
        {
            _statusEffects.Add(type, stackCount);
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }

    public void RemoveStatusEffect(StatusEffectType type, int stackCount)
    {
        if (_statusEffects.ContainsKey(type))
        {
            _statusEffects[type] -= stackCount;
            if(_statusEffects[type] <= 0)
            {
                _statusEffects.Remove(type);
            }
        }
        statusEffectsUI.UpdateStatusEffectUI(type, GetStatusEffectStacks(type));
    }

    public void IncreaseDamageModifier(int amount)
    {
        DamageModifier += amount;
    }
}
