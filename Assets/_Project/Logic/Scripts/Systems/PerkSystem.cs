using System.Collections.Generic;
using UnityEngine;

public class PerkSystem : Singleton<PerkSystem>
{
    private readonly List<Perk> _perks = new();

    public void AddPerk(Perk perk)
    {
        _perks.Add(perk);
        perk.OnAdd();
    }

    public void RemovePerk(Perk perk)
    {
        _perks.Remove(perk);
        perk.OnRemove();
    }
}
