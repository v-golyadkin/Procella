using System;

public class OnCardPlayedCondition : PerkCondition
{
    public override bool SubConditionIsMet(GameAction gameAction)
    {
        if(gameAction is PlayCardGA playCardGA)
        {
            return true;
        }

        return false;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<PlayCardGA>(reaction, timing);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<PlayCardGA>(reaction, timing);
    }

}
