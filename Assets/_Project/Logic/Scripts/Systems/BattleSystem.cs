using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : Singleton<BattleSystem>
{
    private int _currentLevel = 1;

    public int CurrentLevel => _currentLevel;

    [SerializeField] private List<LevelData> levels;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<StartBattleGA>(StartBattlePostReaction, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<KillEnemyGA>(EnemiesKilledPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<StartBattleGA>(StartBattlePostReaction, ReactionTiming.POST);
        ActionSystem.UnsubscribeReaction<KillEnemyGA>(EnemiesKilledPostReaction, ReactionTiming.POST);
    }

    //Reactions
    private void EnemiesKilledPostReaction(KillEnemyGA killEnemyGA)
    {
        if(EnemySystem.Instance.Enemies.Count == 0)
        {
            StartCoroutine(StartNewBattle());
        }
    }

    private void StartBattlePostReaction(StartBattleGA startBattleGA)
    {
        if (CurrentLevel > levels.Count)
        {
            Debug.Log("Finish");
        }
        else
        {
            EnemySystem.Instance.Init(levels[_currentLevel - 1].Enemies);
            _currentLevel++;
        }
    }

    //Helpers

    private IEnumerator StartNewBattle()
    {
        yield return new WaitForEndOfFrame();

        StartBattleGA startBattleGA = new();
        ActionSystem.Instance.Perform(startBattleGA);
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
            //StartBattleGA startBattleGA = new();
            //ActionSystem.Instance.Perform(startBattleGA);

            EnemySystem.Instance.Init(levels[currentLevel - 1].Enemies);
            _currentLevel++;
        }
        
    }
}
