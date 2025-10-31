using System.Collections;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }

    //Performers

    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach(var target in dealDamageGA.Targets)
        {
            target.Damage(dealDamageGA.Damage, dealDamageGA.IgnoredArmour);
            Instantiate(damageVFX, target.transform.position, Quaternion.identity);
            AudioSystem.Instance.PlaySFX("SFX_DEAL_DAMAGE");
            yield return new WaitForSeconds(0.15f);
            if(target.CurrentHelth <= 0)
            {
                if(target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                }
                else
                {
                    target.Heal(target.MaxHealth);
                    KillPlayerGA killPlayerGA = new();
                    ActionSystem.Instance.Perform(killPlayerGA);
                }
            }
        }
    }
}
