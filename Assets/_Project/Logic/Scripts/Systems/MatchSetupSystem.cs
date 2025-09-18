using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private PerkData perkData;
    [SerializeField] private LevelData levelData;
    private void Start()
    {
        //HeroSystem.Instance.Init(heroData);
        //EnemySystem.Instance.Init(levelData.Enemies);
        //CardSystem.Instance.Init(heroData.Deck);
        //PerkSystem.Instance.AddPerk(new Perk(perkData));
        //DrawCardsGA drawCardsGA = new(5);
        //ActionSystem.Instance.Perform(drawCardsGA);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ActionSystem.Instance.ClearAllSubsription();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            EnemySystem.Instance.Init(levelData.Enemies);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnemySystem.Instance.KillAllEnemies();
        }
    }

    public void StartLevel(LevelData levelData)
    {
        EnemySystem.Instance.SpawnEnemy(levelData.Enemies);
    }
}
