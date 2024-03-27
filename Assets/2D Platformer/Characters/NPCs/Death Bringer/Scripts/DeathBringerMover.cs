using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeathBringerMover
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space]
    [SerializeField] private Transform[] _patrolPoints;
    [Space]
    [SerializeField] private float _maxDistanceToEnemy;

    [Header("Vision")]
    [SerializeField] private Transform _visabilityRayPoint;
    [SerializeField] private float _visionDistance;

    private List<Vector2> _cachedPatrollPoints = new List<Vector2>();
    private int _patrolPointIndex = 0;
    private Transform _transform;
    private Character _target;
    private LayerMask _targetLayerMask;

    public int MoveDirectionX { get; private set; } = 0;

    public void Initialize(Transform transform, LayerMask enemyLayerMask)
    {
        _transform = transform;
        _targetLayerMask = enemyLayerMask;

        foreach (Transform point in _patrolPoints)
        {
            _cachedPatrollPoints.Add(point.position);
        }
    }

    public void Move()
    {
        if (_target != null && _target.IsDead)
        {
            _target = null;
        }

        if (_target == null)
        {
            Patroling();
            Rotate(MoveDirectionX, _transform);
        }
        else
        {
            MoveToEnemy();
            LookAt(_target.transform.position);
        }
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }

    public void StopVelocityX()
    {
        _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
    }

    private void Patroling()
    {
        if (TryMoveHorizontalToTarget(_cachedPatrollPoints[_patrolPointIndex].x) == false)
        {
            _patrolPointIndex++;

            if (_patrolPointIndex >= _cachedPatrollPoints.Count)
            {
                _patrolPointIndex = 0;
            }
        }
    }

    private void MoveToEnemy()
    {
        if (Vector2.Distance(_transform.position, _target.transform.position) > _maxDistanceToEnemy)
        {
            TryMoveHorizontalToTarget(_target.transform.position.x);
        }
        else
        {
            MoveDirectionX = 0;
            StopVelocityX();
        }
    }

    private bool TryMoveHorizontalToTarget(float targetX)
    {
        if (targetX - _transform.position.x > 0.1f)
        {
            MoveDirectionX = 1;
        }
        else if (targetX - _transform.position.x < -0.1f)
        {
            MoveDirectionX = -1;
        }
        else
        {
            return false;
        }

        MoveHorizontal(MoveDirectionX);

        return true;
    }

    private void MoveHorizontal(float direction)
    {
        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);
    }

    private void LookAt(Vector2 target)
    {
        Rotate(target.x - _transform.position.x, _transform);
    }

    private void Rotate(float direction, Transform transform)
    {
        if (direction < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void DrawGizmos(Transform transform)
    {
        Gizmos.color = Color.white;

        foreach (Vector2 patrolPoint in _cachedPatrollPoints)
        {
            Gizmos.DrawWireSphere(patrolPoint, 0.2f);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x, _visabilityRayPoint.position.y), _maxDistanceToEnemy);
    }
}
