using UnityEngine;

public class DeathBringer : Character
{
    [Header("Enemy")]
    [SerializeField] private LayerMask _enemyLayerMask;

    [Header("Vision")]
    [SerializeField] private Vision _vision;

    [Header("Movement")]
    [SerializeField] private DeathBringerMover _mover;

    [Header("Combat")]
    [SerializeField] private DeathBringerCombat _combat;
    [SerializeField] private Weapon _weapon;

    [Header("Animation")]
    [SerializeField] private DeathBringerAnimator _animator;
    [SerializeField] private DeathBringerAnimatorEvents _animatorEvents;

    public override void Initialize()
    {
        base.Initialize();
        _mover.Initialize(transform, _enemyLayerMask);
        _combat.Initialize(_weapon.Damage, _enemyLayerMask);
        _vision.SetTargetLayerMask(_enemyLayerMask);
    }

    private void OnEnable()
    {
        Health.OnDecreased += OnHealthDecreased;
        _vision.FoundTarget += _combat.SetTarget;
        _vision.FoundTarget += _mover.SetTarget;

        _animatorEvents.AttackFrame += _combat.AttackMelee;
        _animatorEvents.CastFrame += _combat.CastSpell;
    }

    private void OnDisable()
    {
        Health.OnDecreased -= OnHealthDecreased;
        _vision.FoundTarget -= _combat.SetTarget;
        _vision.FoundTarget -= _mover.SetTarget;

        _animatorEvents.AttackFrame -= _combat.AttackMelee;
        _animatorEvents.CastFrame -= _combat.CastSpell;
    }

    private void Update()
    {
        if (IsDead)
        {
            _mover.StopVelocityX();
            _animator.Death(IsDead);
            DestroyThisObject();

            return;
        }

        _vision.TryFindTarget();

        if(_combat.CanAttackMelee())
        {
            _animator.AttackMele();
        }
        else if (_combat.CanCastSpell())
        {
            _animator.CastSpell();
        }

        _animator.MoveHorizontal(_mover.MoveDirectionX);
    }

    private void FixedUpdate()
    {
        if (_animator.IsAttackClip() == false && _animator.IsCastClip() == false)
        {
            _mover.Move();
        }
        else
        {
            _mover.StopVelocityX();
        }
    }

    private void OnHealthDecreased()
    {
        _animator.TakeDamage();
    }

    private void OnDrawGizmos()
    {
        _mover.DrawGizmos(transform);
        _combat.DrawGizmos();
        _vision.DrawGizmos();
    }
}
