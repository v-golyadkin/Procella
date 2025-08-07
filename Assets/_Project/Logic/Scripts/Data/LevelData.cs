using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level", fileName = "Level")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public EnemyData LeftEnemy { get; private set; }
    [field: SerializeField] public EnemyData MiddleEnemy { get; private set; }
    [field: SerializeField] public EnemyData RightEnemy { get; private set; }

    [field: SerializeField] public List<EnemyData> Enemies { get; private set; }
}
