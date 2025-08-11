using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private PerkData perkData;
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        HeroSystem.Instance.Init(heroData);
        EnemySystem.Instance.Init(levelData.Enemies);
        CardSystem.Instance.Init(heroData.Deck);
        //PerkSystem.Instance.AddPerk(new Perk(perkData));
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }
}
