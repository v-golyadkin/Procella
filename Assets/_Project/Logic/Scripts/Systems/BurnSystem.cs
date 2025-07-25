using System.Collections;
using UnityEngine;

public class BurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject _burnSFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyBurnGA>();
    }

    //Performers

    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        Instantiate(_burnSFX, target.transform.position, Quaternion.identity);
        target.Damage(applyBurnGA.BurnDamage);
        target.RemoveStatusEffect(StatusEffectType.BURN, 1);
        yield return new WaitForSeconds(1f);
    }
}
