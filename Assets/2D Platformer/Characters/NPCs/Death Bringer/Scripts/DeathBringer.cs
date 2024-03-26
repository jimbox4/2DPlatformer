using UnityEngine;

public class DeathBringer : Character
{
    [Header("Enemy")]
    [SerializeField] private LayerMask _enemyLayerMask;
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

        _animatorEvents.AttackFrame += _combat.AttackMelee;
        _animatorEvents.CastFrame += _combat.CastSpell;
    }

    private void OnEnable()
    {
        HealthDecreased += _animator.TakeDamage;
        _mover.FoundEnemy += _combat.SetTarget;
    }

    private void OnDisable()
    {
        HealthDecreased -= _animator.TakeDamage;
        _mover.FoundEnemy -= _combat.SetTarget;
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

        if (_animator.IsAttackClip() == false && _animator.IsCastClip() == false)
        {
            _mover.Move();
        }
        else
        {
            _mover.StopVelocityX();
        }

        if(_combat.CanAttackMele())
        {
            _animator.AttackMele();
        }
        else if (_combat.CanCastSpell())
        {
            _animator.CastSpell();
        }

        _animator.MoveHorizontal(_mover.MoveDirectionX);
    }

    private void OnDrawGizmos()
    {
        //_mover.DrawGizmos(transform);
        _combat.DrawGizmos();
    }
}
