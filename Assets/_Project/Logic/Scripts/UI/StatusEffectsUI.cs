using System.Collections.Generic;
using UnityEngine;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUIPrefab;
    [SerializeField] private Sprite armourSprite, burnSprite, poisonSprite;

    private Dictionary<StatusEffectType, StatusEffectUI> _statusEffectUIs = new();

    public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
    {
        if(stackCount == 0)
        {
            if (_statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = _statusEffectUIs[statusEffectType];
                _statusEffectUIs.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!_statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = Instantiate(statusEffectUIPrefab, transform);
                _statusEffectUIs.Add(statusEffectType, statusEffectUI);
            }
            Sprite sprite = GetSpriteByType(statusEffectType);
            _statusEffectUIs[statusEffectType].Set(sprite, stackCount);
        }
    }

    private Sprite GetSpriteByType(StatusEffectType statusEffectType)
    {
        return statusEffectType switch
        {
            StatusEffectType.ARMOR => armourSprite,
            StatusEffectType.BURN => burnSprite,
            StatusEffectType.POISON => poisonSprite,
            _ => null,
        };
    }
}
