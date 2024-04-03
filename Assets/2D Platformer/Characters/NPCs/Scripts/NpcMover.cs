using UnityEngine;

public abstract class NpcMover : CharacterMover
{
    public int MoveDirectionX { get; private set; } = 0;

    protected Transform Transform;

    public virtual void Initialize(Rigidbody2D rigidbody2D, Transform transform)
    {
        Initialize(rigidbody2D);
        Transform = transform;
    }

    protected bool TryMoveHorizontalToTarget(float targetX)
    {
        if (targetX - Transform.position.x > 0.1f)
        {
            MoveDirectionX = 1;
        }
        else if (targetX - Transform.position.x < -0.1f)
        {
            MoveDirectionX = -1;
        }
        else
        {
            return false;
        }

        MoveHorizontal(MoveDirectionX);

        return true;
    }

    protected void LookAt(Vector2 target)
    {
        Rotate(target.x - Transform.position.x, Transform);
    }

    protected void ResetDirectionX()
    {
        MoveDirectionX = 0;
    }
}
