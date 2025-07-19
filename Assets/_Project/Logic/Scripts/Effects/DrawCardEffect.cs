using System.Collections.Generic;
using UnityEngine;

public class DrawCardEffect : Effect
{
    [SerializeField] private int _drawAmount;
    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        DrawCardsGA drawCardsGA = new(_drawAmount);
        return drawCardsGA;
    }
}
