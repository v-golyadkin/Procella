using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    public void OnClick()
    {
        PlayerTurnEndGA playerTurnEndGA = new();
        ActionSystem.Instance.Perform(playerTurnEndGA);
    }
}
