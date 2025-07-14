using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private List<CardData> _deckData;

    private void Start()
    {
        CardSystem.Instance.Setup(_deckData);
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }
}
