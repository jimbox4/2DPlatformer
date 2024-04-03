using System;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[Serializable]
public class Pursuit : NpcMover
{
    [SerializeField] private float _maxDistanceToEnemy;

    private Character _target;

    public void Move()
    {
        if (Vector2.Distance(Transform.position, _target.transform.position) > _maxDistanceToEnemy)
        {
            TryMoveHorizontalToTarget(_target.transform.position.x);
            LookAt(_target.transform.position);
        }
        else
        {
            ResetDirectionX();
            ResetVelocityX();
        }
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }

    public void DrawGizmos(UnityEngine.Transform transform)
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _maxDistanceToEnemy);
    }
}
