using System;
using System.Threading.Tasks;
using UnityEngine;

[SelectionBase]
public abstract class Character : MonoBehaviour
{
    [SerializeField, Min(0)] protected float _detroyDelay;
    [Header("Stats")]
    [SerializeField] private Health _health;

    protected bool IsDead;
    protected string Name;

    public event Action HealthDecreased;

    public virtual void Initialize()
    {
        IsDead = false;
        Name = gameObject.name;
    }

    public void TakeHealth(int healthPoints)
    {
        if (_health.TryIncrease(healthPoints))
        {
            Debug.Log($"{Name} take heal {healthPoints}");
        }
    }

    public void TakeDamage(int damage)
    {
        if (_health.TryDecrease(damage) == false)
        {
            return;
        }

        HealthDecreased.Invoke();
        Debug.Log($"{Name} take {damage} damage");

        if (_health.CurrentValue == 0)
        {
            IsDead = true;

            Debug.Log($"{Name} is dead");
        }
    }

    protected virtual void DestroyMe()
    {
        int miliseconds = (int)_detroyDelay * 1000;
        Task.Delay(miliseconds);
        enabled = false;
        Destroy(gameObject, _detroyDelay + 0.1f);
    }
}
