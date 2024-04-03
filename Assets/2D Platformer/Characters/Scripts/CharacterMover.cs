using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class CharacterMover
{
    [SerializeField] private float _speed;

    protected Rigidbody2D Rigidbody2D;
    
    public virtual void Initialize(Rigidbody2D rigidbody2D)
    {
        Rigidbody2D = rigidbody2D;
    }

    public void ResetVelocityX()
    {
        Rigidbody2D.velocity = new Vector2(0, Rigidbody2D.velocity.y);
    }

    protected void MoveHorizontal(float direction)
    {
        Rigidbody2D.velocity = new Vector2(direction * _speed, Rigidbody2D.velocity.y);
    }

    protected void Rotate(float direction, Transform transform)
    {
        if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
