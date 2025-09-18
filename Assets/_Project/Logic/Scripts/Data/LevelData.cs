using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level", fileName = "Level")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public List<EnemyData> Enemies { get; private set; }
}

