using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField, Min(0)] private float _jumpForce;
    
    protected Rigidbody2D Rigidbody { get; private set; }
    protected int DirectionX { get; private set; } = 0;

    private Transform _transform;
    private int _jumpForceScale = 50;

    protected virtual void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }

    protected void MoveHorizontal(float direction)
    {
        Rigidbody.velocity = new Vector2(direction * _speed, Rigidbody.velocity.y);

        Rotate(direction);
    }

    protected bool TryMoveHorizontalToTarget(float targetX)
    {
        if (targetX - transform.position.x > 0.1f)
        {
            DirectionX = 1;
        }
        else if (targetX - transform.position.x < -0.1f)
        {
            DirectionX = -1;
        }
        else
        {
            return false;
        }

        MoveHorizontal(DirectionX);

        return true;
    }

    protected void StopVelocytyX()
    {
        Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
    }

    protected void Jump()
    {
        Rigidbody.AddForce(Vector2.up * _jumpForce * _jumpForceScale);
    }

    private void Rotate(float direction)
    {
        if (direction < 0)
        {
            _transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction > 0)
        {
            _transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
