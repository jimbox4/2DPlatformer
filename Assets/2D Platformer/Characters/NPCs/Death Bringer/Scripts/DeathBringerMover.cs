using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeathBringerMover
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space]
    [SerializeField] private Transform[] _patrolPoints;
    [Space]
    [SerializeField] private float _speed;

    private List<Vector2> _cachedPatrollPoints = new List<Vector2>();
    private int _patrolPointIndex = 0;
    private Transform _transform;

    public int DirectionX { get; private set; } = 0;

    public void Initialize(Transform transform)
    {
        _transform = transform;

        foreach (Transform point in _patrolPoints)
        {
            _cachedPatrollPoints.Add(point.position);
        }
    }

    public void Move()
    {
        Patroling();
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

    protected bool TryMoveHorizontalToTarget(float targetX)
    {
        if (targetX - _transform.position.x > 0.1f)
        {
            DirectionX = 1;
        }
        else if (targetX - _transform.position.x < -0.1f)
        {
            DirectionX = -1;
        }
        else
        {
            return false;
        }

        MoveHorizontal(DirectionX);

        return true;
    }

    private void MoveHorizontal(float direction)
    {
        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);

        Rotate(direction, _transform);
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
}
