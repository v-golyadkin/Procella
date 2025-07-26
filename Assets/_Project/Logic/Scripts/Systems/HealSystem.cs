using System.Collections;
using UnityEngine;

public class HealSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<HealGA>(HealPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<HealGA>();
    }

    // Performers

    private IEnumerator HealPerformer(HealGA healGA)
    {
        foreach(var target in healGA.Targets)
        {
            target.Heal(healGA.Amount);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
