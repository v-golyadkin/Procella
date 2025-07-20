using UnityEngine;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private ArrowView _arrowView;
    [SerializeField] private LayerMask _targetLayerMask;

    public void StartTargeting(Vector3 startPosition)
    {
        _arrowView.gameObject.SetActive(true);
        _arrowView.SetupArrow(startPosition);
    }

    public EnemyView EndTargeting(Vector3 endPosition)
    {
        _arrowView.gameObject.SetActive(false);
        if(Physics.Raycast(endPosition, Vector3.forward, out RaycastHit hit, 10f, _targetLayerMask)
            && hit.collider != null
            && hit.transform.TryGetComponent(out EnemyView enemyView))
        {
            return enemyView;
        }

        return null;
    }
}
