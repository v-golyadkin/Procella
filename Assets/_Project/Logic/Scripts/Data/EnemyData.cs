using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "Enemy")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public Sprite Image {  get; private set; }
    [field: SerializeField] public int Health {  get; private set; }
    [field: SerializeField] public int AttackPower {  get; private set; }
    [field: SerializeField] public int Armour {  get; private set; }
}
