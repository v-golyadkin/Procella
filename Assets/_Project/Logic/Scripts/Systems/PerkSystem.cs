using System.Collections.Generic;
using UnityEngine;

public class PerkSystem : Singleton<PerkSystem>
{
    [SerializeField] private PerksUI _perksUI;
    private readonly List<Perk> _perks = new();

    public void AddPerk(Perk perk)
    {
        _perks.Add(perk);
        _perksUI.AddPerkUI(perk);
        perk.OnAdd();
    }

    public void RemovePerk(Perk perk)
    {
        _perks.Remove(perk);
        _perksUI.RemovePerkUI(perk);
        perk.OnRemove();
    }
}
