using UnityEngine;

public class DeathBringer : Character
{
    [Header("Enemy")]
    [SerializeField] private LayerMask _enemyLayerMask;

    [Header("Vision")]
    [SerializeField] private Vision _vision;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D _rigidbody2d;
    [SerializeField] private Patrolling _patrollin;
    [SerializeField] private Pursuit _pursuit;

    [Header("Combat")]
    [SerializeField] private DeathBringerCombat _combat;

    [Header("Animation")]
    [SerializeField] private DeathBringerAnimator _animator;
    [SerializeField] private DeathBringerAnimatorEvents _animatorEvents;

    public override void Initialize()
    {
        base.Initialize();
        _pursuit.Initialize(_rigidbody2d, transform);
        _patrollin.Initialize(_rigidbody2d, transform);
        _combat.Initialize(_enemyLayerMask);
        _vision.SetTargetLayerMask(_enemyLayerMask);
    }

    private void OnEnable()
    {
        Health.OnDecreased += OnHealthDecreased;
        _combat.TargetDefiated += _vision.ResetTarget;

        _vision.FindTarget += _combat.SetTarget;
        _vision.FindTarget += _pursuit.SetTarget;

        _animatorEvents.AttackFrame += _combat.AttackMelee;
        _animatorEvents.CastFrame += _combat.CastSpell;
    }

    private void OnDisable()
    {
        Health.OnDecreased -= OnHealthDecreased;
        _combat.TargetDefiated -= _vision.ResetTarget;

        _vision.FindTarget -= _combat.SetTarget;
        _vision.FindTarget -= _pursuit.SetTarget;

        _animatorEvents.AttackFrame -= _combat.AttackMelee;
        _animatorEvents.CastFrame -= _combat.CastSpell;
    }

    private void Update()
    {
        if (IsDead)
        {
            _pursuit.ResetVelocityX();
            _animator.Death(IsDead);
            DestroyThisObject();

            return;
        }

        _vision.TryFindTarget();

        if (_combat.CanAttackMelee())
        {
            _animator.AttackMele();
        }
        else if (_combat.CanCastSpell())
        {
            _animator.CastSpell();
        }

        if (_vision.HasTarget)
        {
            _animator.MoveHorizontal(_pursuit.MoveDirectionX);
        }
        else
        {
            _animator.MoveHorizontal(_patrollin.MoveDirectionX);
        }
    }

    private void FixedUpdate()
    {
        if (_animator.IsAttackClip() || _animator.IsCastClip())
        {
            _pursuit.ResetVelocityX();

            return;
        }

        if (_vision.HasTarget)
        {
            _pursuit.Move();
        }
        else
        {
            _patrollin.Move();
        }
    }

    private void OnHealthDecreased()
    {
        _animator.TakeDamage();
    }

    private void OnDrawGizmos()
    {
        _pursuit.DrawGizmos(transform);
        _patrollin.DrawGizmos();
        _combat.DrawGizmos();
        _vision.DrawGizmos();
    }
}
