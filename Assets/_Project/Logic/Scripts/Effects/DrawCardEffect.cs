using UnityEngine;

public class DrawCardEffect : Effect
{
    [SerializeField] private int _drawAmount;
    public override GameAction GetGameAction()
    {
        DrawCardsGA drawCardsGA = new(_drawAmount);
        return drawCardsGA;
    }
}
