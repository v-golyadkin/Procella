using System;
using UnityEngine;

public class OnEnemyAttackCondition : PercCondition
{
    public override bool SubConditionIsMet()
    {
        return true;
    }

    public override void SubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.SubscribeReaction<AttackHeroGA>(reaction, timing);
    }

    public override void UnsubscribeCondition(Action<GameAction> reaction)
    {
        ActionSystem.UnsubscribeReaction<AttackHeroGA>(reaction, timing);
    }
}
