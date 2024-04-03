using System.Collections;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Bag _bag;

    [Header("Interact with enviroment")]
    [SerializeField] private InteractSystem _interactionSystem;
    [SerializeField] private CollisionHandler _collisionHandler;

    [Header("Movement")]
    [SerializeField] private PlayerMover _mover;

    [Header("Player combat system")]
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private Weapon _weapon;

    [Header("Skills")]
    [SerializeField] private VampireSkill _vampireSkill;

    [Header("Animation")]
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerAnimatorEvents _animatorEvents;

    private UserInput _input;
    private float _nextAttackTime;
    private float direction;

    public override void Initialize()
    {
        base.Initialize();
        _mover.Initialize(GetComponent<Rigidbody2D>());
        _interactionSystem.Initialize(gameObject.transform);
        _vampireSkill.Initialize(this);

        _input = new UserInput();

        _input.Player.Interact.performed += interactingAction => _interactionSystem.Interact();
        _input.Player.Attack.performed += attackAction => StartAnimatorAttackState();
        _input.Player.Jump.performed += jumpAction => _mover.Jump();
        _input.Player.VampirizmSkill.performed += vampireSkillAction => UseVampirizeSkill();

        _nextAttackTime = 0 - _combat.AttackDelay;
    }

    private void OnEnable()
    {
        _input.Enable();
        _animatorEvents.AttackFrame += _combat.Attack;
        _vampireSkill.OnSkillEnded += ReplenishVampirizmSkill;
        Health.OnDecreased += OnHealthDecreased;
    }

    private void OnDisable()
    {
        _input.Disable();
        _animatorEvents.AttackFrame -= _combat.Attack;
        _vampireSkill.OnSkillEnded -= ReplenishVampirizmSkill;
        Health.OnDecreased -= OnHealthDecreased;
    }

    private void Update()
    {
        if (IsDead)
        {
            _vampireSkill.Stop();
            _mover.ResetVelocityX();
            _animator.Death(IsDead);
            DestroyThisObject();
            return;
        }

        direction = _input.Player.Move.ReadValue<float>();

        _animator.MoveHorizontal(direction);
        _animator.MoveVertical(_mover.IsOnGround, _mover.VelocityY);
    }

    private void FixedUpdate()
    {
        _mover.Move(direction, transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_collisionHandler.TryHandleCoinCollision(collision, out int coinPoints))
        {
            _bag.IncreaseCoins(coinPoints);
        }
        else if (HasMaxHealth == false && _collisionHandler.TryHandleHeartCollision(collision, out int heartPoints))
        {
            Heal(heartPoints);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactionSystem.TryShowInteractKey(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interactionSystem.HideInteractKey(collision);
    }

    private void StartAnimatorAttackState()
    {
        if (_nextAttackTime < Time.time)
        {
            _animator.AttackMelee();
            _nextAttackTime = Time.time + _combat.AttackDelay;
        }
    }

    private void UseVampirizeSkill()
    {
        if (_vampireSkill.CanActivate)
        {
            StartCoroutine(_vampireSkill.Vampirize());
        }
    }

    private void ReplenishVampirizmSkill()
    {
        StartCoroutine(_vampireSkill.ReplenishAbility());
    }

    private void OnHealthDecreased()
    {
        _animator.TakeDamage();
    }

    private void OnDrawGizmos()
    {
        _mover.DrawGizmos();
        _combat.DrawGizmos();
        _vampireSkill.DrawGizmos();
    }
}
