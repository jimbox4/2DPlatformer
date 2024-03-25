using UnityEditor.Timeline.Actions;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Bag _bag;

    [Header("Interact with enviroment")]
    [SerializeField] private InteractSystem _interactionSystem;

    [Header("Movement")]
    [SerializeField] private PlayerMover _mover;

    [Header("Player combat system")]
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private Weapon _weapon;

    [Header("Animation")]
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerAnimatorEvents _animatorEvents;

    private UserInput _input;
    private float _attackTime = 0;

    public override void Initialize()
    {
        base.Initialize();
        _combat.Initialize(_weapon.Damage);
        _interactionSystem.Initialize(gameObject.transform);

        _input = new UserInput();

        _input.Player.Interact.performed += interactingAction => _interactionSystem.Interact();
        _input.Player.Attack.performed += attackAction => StartAnimatorAttackState();
        _input.Player.Jump.performed += jumpAction => _mover.Jump();

        
    }

    private void OnEnable()
    {
        _input.Enable();
        _animatorEvents.AttackFrame += _combat.Attack;
        HealthDecreased += _animator.TakeDamage;
    }

    private void OnDisable()
    {
        _input.Disable();
        _animatorEvents.AttackFrame -= _combat.Attack;
        HealthDecreased -= _animator.TakeDamage;
    }

    private void Update()
    {
        if (IsDead)
        {
            _mover.StopHorizontalVelocity();
            _animator.Death(IsDead);
            DestroyMe();
            return;
        }

        float direction = _input.Player.Move.ReadValue<float>();

        _mover.Move(direction, transform);

        _animator.MoveHorizontal(direction);
        _animator.MoveVertical(_mover.IsOnGround, _mover.VelocityY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            _bag.IncreaseCoins(coin.Collect());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactionSystem.ShowInteractKey(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactionSystem.HideInteractKey(collision);
    }

    private void StartAnimatorAttackState()
    {
        if (Time.time - _attackTime > _combat.AttackDelay)
        {
            _animator.AttackEnemy();
            _attackTime = Time.time;
        }
    }

    private void OnDrawGizmos()
    {
        _mover.DrawGizmos();
        _combat.DrawGizmos();
    }
}
