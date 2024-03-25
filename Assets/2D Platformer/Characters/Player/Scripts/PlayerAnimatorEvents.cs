using System;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    public event Action AttackFrame;

    public void Attack()
    {
        AttackFrame.Invoke();
    }
}
