using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Patrolling : NpcMover
{
    [Space]
    [SerializeField] private Transform[] _patrolPoints;
    [Space]

    private List<Vector2> _cachedPatrollPoints = new List<Vector2>();
    private int _patrolPointIndex = 0;

    public override void Initialize(Rigidbody2D rigidbody2D, Transform transform)
    {
        base.Initialize(rigidbody2D, transform);

        foreach (Transform point in _patrolPoints)
        {
            _cachedPatrollPoints.Add(point.position);
        }
    }

    public void Move()
    {
        if (TryMoveHorizontalToTarget(_cachedPatrollPoints[_patrolPointIndex].x) == false)
        {
            _patrolPointIndex++;

            if (_patrolPointIndex >= _cachedPatrollPoints.Count)
            {
                _patrolPointIndex = 0;
            }
        }

        Rotate(MoveDirectionX, Transform);
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.white;

        foreach (Vector2 patrolPoint in _cachedPatrollPoints)
        {
            Gizmos.DrawWireSphere(patrolPoint, 0.2f);
        }
    }
}
