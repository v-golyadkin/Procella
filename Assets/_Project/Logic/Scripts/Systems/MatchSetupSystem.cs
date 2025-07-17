using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private HeroData _heroData;
    [SerializeField] private List<EnemyData> _enemyDatas;
    private void Start()
    {
        HeroSystem.Instance.Setup(_heroData);
        EnemySystem.Instance.Setup(_enemyDatas);
        CardSystem.Instance.Setup(_heroData.Deck);
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }
}
