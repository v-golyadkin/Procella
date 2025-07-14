using System.Collections.Generic;
using UnityEngine;

public class TestSystem : MonoBehaviour
{
    [SerializeField] private List<CardData> _deckData;

    private void Start()
    {
        CardSystem.Instance.Setup(_deckData);
    }
}
