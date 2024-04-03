using System;
using UnityEngine;
using UnityEngine.Pool;

[Serializable]
public class DeathBringerCombat
{
    [Header("Melee attack")]
    [SerializeField, Min(0)] private int _meleeAttackDamage;
    [SerializeField, Min(0)] private float _meleeAttackDelay;
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField, Min(0)] private float _meleeAttackDistance;
    [SerializeField, Min(0)] private float _attackRadius;

    [Header("Spell attack")]
    [SerializeField, Min(0)] private int _spellDamage;
    [SerializeField, Min(0)] private float _castDelay;
    [SerializeField] Transform _castPoint;
    [SerializeField, Min(0)] private float _castRadius;
    [SerializeField] private DeathBringerSpell _spellPrefab;

    private ObjectPool<DeathBringerSpell> _pool;
    private Character _target = null;
    private LayerMask _targetLayerMask;
    private float _nextCastTime = 0;
    private float _nextAttackTime = 0;

    public event Action TargetDefiated;

    public void Initialize(LayerMask enemyLayerMask)
    {
        _pool = new ObjectPool<DeathBringerSpell>(
        createFunc: () =>
        {
            var spell = GameObject.Instantiate(_spellPrefab);
            var returToPool = spell.GetComponent<SpellReturnToPool>();
            spell.Initialize(_spellDamage);
            returToPool.Initialize(_pool, spell);
            return spell;
        },
        actionOnGet: (spell) => ActionOnGet(spell),
        actionOnRelease: (spell) => spell.gameObject.SetActive(false),
        actionOnDestroy: (spell) => GameObject.Destroy(spell),
        defaultCapacity: 2
        );

        _targetLayerMask = enemyLayerMask;
    }

    public void SetTarget(Character target)
    {
        _target = target;
    }

    private void ActionOnGet(DeathBringerSpell spell)
    {
        spell.transform.position = _target.transform.position;
        spell.gameObject.SetActive(true);
    }

    public bool CanAttackMelee()
    {
        IsTargetDead();

        if (_nextAttackTime > Time.time || _target == null)
        {
            return false;
        }

        _nextAttackTime = Time.time + _meleeAttackDelay;

        RaycastHit2D hit = Physics2D.Raycast(_meleeAttackPoint.position, _meleeAttackPoint.right, _meleeAttackDistance, _targetLayerMask);

        return hit;
    }

    public void AttackMelee()
    {
        Collider2D collider = Physics2D.OverlapCircle(new Vector2(_meleeAttackPoint.position.x + _meleeAttackPoint.right.x * _meleeAttackDistance * 0.5f, _meleeAttackPoint.position.y), _attackRadius, _targetLayerMask);

        if (collider != null && collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(_meleeAttackDamage);
        }
    }

    public bool CanCastSpell()
    {
        IsTargetDead();

        if (_nextCastTime > Time.time || _target == null)
        {
            return false;
        }

        _nextCastTime = Time.time + _castDelay;

        if (Vector2.Distance(_target.transform.position, _castPoint.position) < _castRadius)
        {
            return true;
        }

        return false;
    }

    public void CastSpell()
    {
        _pool.Get();
    }

    private void IsTargetDead()
    {
        if (_target != null && _target.IsDead)
        {
            TargetDefiated.Invoke();
        }
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(_meleeAttackPoint.position.x + _meleeAttackPoint.right.x * _meleeAttackDistance * 0.5f, _meleeAttackPoint.position.y), _attackRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(_meleeAttackPoint.position, _meleeAttackPoint.right * new Vector2(_meleeAttackDistance * 0.5f + _attackRadius, 0));

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_castPoint.position, _castRadius);
    }
}
