using System;
using UnityEngine;

[Serializable]
public class Vision
{
    [SerializeField] private Transform _visabilityRayPoint;
    [SerializeField] private float _visionDistance;

    private Character _target;
    private LayerMask _targetLayerMask;

    public event Action<Character> FindTarget;

    public bool HasTarget => _target != null;

    public bool TryFindTarget()
    {
        if (_target != null)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(_visabilityRayPoint.position, _visabilityRayPoint.right, _visionDistance, _targetLayerMask);

        if (hit && hit.collider.TryGetComponent(out Character player))
        {
            _target = player;
            FindTarget.Invoke(_target);

            return true;
        }

        return false;
    }

    public void ResetTarget()
    {
        _target = null;
        FindTarget.Invoke(_target);
    }

    public void SetTargetLayerMask(LayerMask targetLayerMask)
    {
        _targetLayerMask = targetLayerMask;
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_visabilityRayPoint.position + new Vector3(0, 0.02f, 0), _visabilityRayPoint.right * _visionDistance);
    }
}
