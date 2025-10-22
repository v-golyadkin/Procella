using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : Singleton<BattleSystem>
{
    private int _currentLevel = 1 ;

    public int CurrentLevel => _currentLevel;

    [SerializeField] private List<LevelData> levels;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<KillEnemyGA>(OnEnemiesKilled, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<KillEnemyGA>(OnEnemiesKilled, ReactionTiming.POST);
    }

    //Reactions
    private void OnEnemiesKilled(KillEnemyGA killEnemyGA)
    {
        if(EnemySystem.Instance.Enemies.Count == 0)
        {
            Debug.Log("Enemies Die");         
            StartBattle(_currentLevel);
        }
    }

    public void StartBattle(int currentLevel = 1)
    {
        Debug.Log($"Current level:{CurrentLevel}");
        Debug.Log($"Level count:{levels.Count}");

        if (CurrentLevel > levels.Count)
        {
            Debug.Log("Finish");
            return;
        }
        else
        {
            StartBattleGA startBattleGA = new();
            ActionSystem.Instance.Perform(startBattleGA);

            EnemySystem.Instance.Init(levels[currentLevel - 1].Enemies);
            _currentLevel++;
        }
        
    }
}
