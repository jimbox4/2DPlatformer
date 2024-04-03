using System;
using UnityEngine;

[Serializable]
public class PlayerCombat
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRadius;
    [SerializeField] private LayerMask _enemyLayer;

    public float AttackDelay => _attackDelay;

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius, _enemyLayer);

        if (colliders == null)
        {
            return;
        }

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Character character))
            {
                character.TakeDamage(_damage);
            }
        }
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRadius);
    }
}
