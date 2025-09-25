using System.Collections;
using UnityEngine;

public class StatusEffectSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
        ActionSystem.AttachPerformer<RemoveStatusEffectGA>(RemoveStatusEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();
        ActionSystem.DetachPerformer<RemoveStatusEffectGA>();
    }

    //Performers

    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGA)
    {
        foreach(var target in addStatusEffectGA.Targets)
        {
            target.AddStatusEffect(addStatusEffectGA.StatusEffectType, addStatusEffectGA.StackCount);
            yield return null;
            //TO DO ADD VFX for adding status effect
        }
    }

    private IEnumerator RemoveStatusEffectPerformer(RemoveStatusEffectGA removeStatusEffectGA)
    {
        foreach(var target in removeStatusEffectGA.Targets)
        {
            target.RemoveStatusEffect(removeStatusEffectGA.StatusEffectsType, target.GetStatusEffectStacks(removeStatusEffectGA.StatusEffectsType));
            yield return null;
        }
    }
}
