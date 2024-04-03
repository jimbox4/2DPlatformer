using System;
using UnityEngine;

[Serializable]
public class PlayerMover : CharacterMover
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField, Min(0)] private float _jumpForce;
    [Header("Ground check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckPointSize;

    private int _jumpForceScale = 50;

    public bool IsOnGround { get; private set; }

    public float VelocityY => _rigidbody2D.velocity.y;

    public void Initialize()
    {
        Initialize(_rigidbody2D);
    }

    public void Move(float direction, Transform transform)
    {
        Rotate(direction, transform);
        MoveHorizontal(direction);

        IsOnGround = CheckGround();
    }

    public void Jump()
    {
        if (IsOnGround == false)
        {
            return;
        }

        _rigidbody2D.AddForce(Vector2.up * _jumpForce * _jumpForceScale);
    }

    public bool CheckGround()
    {
        var boxcast = Physics2D.BoxCast(_groundCheckPoint.position, _groundCheckPointSize, 0, Vector2.down, 0, _groundLayer);

        return boxcast.collider != null;
    }

    public void DrawGizmos()
    {
        Gizmos.color = new Color(16, 16, 16);
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckPointSize);
    }
}
