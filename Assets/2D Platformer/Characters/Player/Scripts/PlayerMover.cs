using System;
using UnityEngine;

[Serializable]
public class PlayerMover
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    [SerializeField, Min(0)] private float _jumpForce;
    [Header("Ground check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckPointSize;

    private int _jumpForceScale = 50;

    public bool IsOnGround { get; private set; }

    public float VelocityY => _rigidbody.velocity.y;

    public void Move(float direction, Transform transform)
    {
        Rotate(direction, transform);
        _rigidbody.velocity = new Vector2(direction * _speed, _rigidbody.velocity.y);

        IsOnGround = CheckGround();
    }

    public void Jump()
    {
        if (IsOnGround == false)
        {
            return;
        }

        _rigidbody.AddForce(Vector2.up * _jumpForce * _jumpForceScale);
    }

    public bool CheckGround()
    {
        var boxcast = Physics2D.BoxCast(_groundCheckPoint.position, _groundCheckPointSize, 0, Vector2.down, 0, _groundLayer);

        return boxcast.collider != null;
    }

    public void StopHorizontalVelocity()
    {
        _rigidbody.velocity = Vector2.zero;
    }

    private void Rotate(float direction, Transform transform)
    {
        if (direction < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (direction > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void DrawGizmos()
    {
        Gizmos.color = new Color(16, 16, 16);
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckPointSize);
    }
}
