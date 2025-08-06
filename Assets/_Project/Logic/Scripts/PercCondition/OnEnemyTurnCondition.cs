using System;

public class OnEnemyTurnCondition : PerkCondition
{
    public override bool SubConditionIsMet(GameAction gameAction)
    {
        return true;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(reaction, timing);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(reaction, timing);
    }
}
