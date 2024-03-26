using UnityEngine;

public class DeathBringerAnimator : MonoBehaviour
{
    private const string DirectionX = nameof(DirectionX);
    private const string Cast = nameof(Cast);
    private const string Attack = nameof(Attack);
    private const string Hurt = nameof(Hurt);
    private const string IsDead = nameof(IsDead);

    [SerializeField] private Animator _animator;

    public bool IsAttackClip()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(Attack);
    }

    public bool IsCastClip()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).IsName(Cast);
    }

    public void MoveHorizontal(int direction)
    {
        _animator.SetInteger(DirectionX, direction);
    }

    public void CastSpell()
    {
        _animator.SetTrigger(Cast);
    }

    public void AttackMele()
    {
        _animator.SetTrigger(Attack);
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(Hurt);
    }

    public void Death(bool isDead)
    {
        _animator.SetBool(IsDead, isDead);
    }
}
