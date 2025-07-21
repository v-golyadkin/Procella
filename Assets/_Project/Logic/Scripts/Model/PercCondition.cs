using System;
using UnityEngine;

public abstract class PercCondition
{
    [SerializeField] protected ReactionTiming timing;

    public abstract void SubscribeCondition(Action<GameAction> reaction);
    public abstract void UnsubscribeCondition(Action<GameAction> reaction);
    public abstract bool SubConditionIsMet();
}
