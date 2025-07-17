using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemyBoardView _enemyBoardView;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerform);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach(var enemyData in enemyDatas)
        {
            _enemyBoardView.AddEnemy(enemyData);
        }
    }

    private IEnumerator EnemyTurnPerform(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn");
        yield return new WaitForSeconds(2f);
        Debug.Log("End Enemy Turn"); 
    }
}
