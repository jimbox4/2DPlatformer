using System;
using UnityEngine;

public class DeathBringerAnimatorEvents : MonoBehaviour
{
    public event Action AttackFrame;
    public event Action CastFrame;

    public void Attack()
    {
        AttackFrame.Invoke();
    }

    public void Cast()
    {
        CastFrame.Invoke();
    }
}
