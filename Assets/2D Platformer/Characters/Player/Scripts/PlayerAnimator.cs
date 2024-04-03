using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string DirectionX = nameof(DirectionX);
    private const string VelocityY = nameof(VelocityY);
    private const string Attack = nameof(Attack);
    private const string Hurt = nameof(Hurt);
    private const string IsDead = nameof(IsDead);
    private const string IsOnGround = nameof(IsOnGround);

    [SerializeField] private Animator _animator;

    public void MoveHorizontal(float direction)
    {
        _animator.SetFloat(DirectionX, Mathf.Abs(direction));

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            _animator.speed = Mathf.Abs(direction);
        }
        else
        {
            _animator.speed = 1;
        }
    }

    public void MoveVertical(bool isOnGround, float velocityY)
    {
        _animator.SetBool(IsOnGround, isOnGround);
        _animator.SetFloat(VelocityY, velocityY);
    }

    public void AttackMelee()
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
