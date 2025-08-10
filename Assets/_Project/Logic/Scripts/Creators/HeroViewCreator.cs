using UnityEngine;

public class HeroViewCreator : Singleton<HeroViewCreator>
{
    [SerializeField] private HeroView heroViewPrefab;
    [SerializeField] private Transform heroSpawnPoint;

    public HeroView CreateHeroView(HeroData heroData)
    {
        HeroView heroView = Instantiate(heroViewPrefab, heroSpawnPoint.position, heroSpawnPoint.rotation);
        heroView.Setup(heroData);
        return heroView;
    }
}
