using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private PerkData perkData;
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        HeroSystem.Instance.Init(heroData);
        CardSystem.Instance.Init(heroData.Deck);
 
        StartBattleGA startBattleGA = new StartBattleGA();
        ActionSystem.Instance.Perform(startBattleGA);
        
        PerkSystem.Instance.AddPerk(new Perk(perkData));
    }
}
