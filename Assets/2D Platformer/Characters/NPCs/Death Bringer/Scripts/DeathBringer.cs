using UnityEngine;

public class DeathBringer : Character
{
    [Header("Movement")]
    [SerializeField] private DeathBringerMover _mover;

    [Header("Animation")]
    [SerializeField] private DeathBringerAnimator _animator;

    public override void Initialize()
    {
        base.Initialize();
        _mover.Initialize(transform);
    }

    private void Update()
    {
        _mover.Move();
        _animator.MoveHorizontal(_mover.DirectionX);
    }
}
