using System.Collections;
using UnityEngine;

public class HealSystem : MonoBehaviour
{
    [SerializeField] private GameObject healVFX;

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
            if(target.CurrentHelth > 0)
            {
                yield return new WaitForSeconds(0.1f);
                Instantiate(healVFX, target.transform.position, Quaternion.identity);
                target.Heal(healGA.Amount);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}
