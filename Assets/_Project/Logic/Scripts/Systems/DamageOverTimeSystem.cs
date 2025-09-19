using System.Collections;
using UnityEngine;

public class DamageOverTimeSystem : MonoBehaviour
{
    [SerializeField] private GameObject burnSFX;
    [SerializeField] private GameObject poisonSFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
        ActionSystem.AttachPerformer<ApplyPoisonGA>(ApplyPoisonPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyBurnGA>();
        ActionSystem.DetachPerformer<ApplyPoisonGA>();
    }

    //Performers

    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        Instantiate(burnSFX, target.transform.position, Quaternion.identity);
        AudioSystem.Instance.PlaySFX("SFX_SE_BURN");
        target.Damage(applyBurnGA.BurnDamage, false);
        target.RemoveStatusEffect(StatusEffectType.BURN, 1);

        yield return new WaitForSeconds(0.2f);

        if(target.CurrentHelth <= 0)
        {
            if(target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }
        }
    }

    private IEnumerator ApplyPoisonPerformer(ApplyPoisonGA applyPoisonGA)
    {
        CombatantView target = applyPoisonGA.Target;
        Instantiate (poisonSFX, target.transform.position , Quaternion.identity);
        AudioSystem.Instance.PlaySFX("SFX_DEAL_DAMAGE");
        target.Damage(applyPoisonGA.PoisonDamage, true);
        target.RemoveStatusEffect(StatusEffectType.POISON, 1);

        yield return new WaitForSeconds(0.2f);

        if (target.CurrentHelth <= 0)
        {
            if (target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }
        }
    }
}
