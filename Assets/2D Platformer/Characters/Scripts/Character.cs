using System;
using System.Threading.Tasks;
using UnityEngine;

[SelectionBase]
public abstract class Character : MonoBehaviour
{
    [SerializeField, Min(0)] protected float _detroyDelay;
    [Header("Stats")]
    [SerializeField] private Health _health;

    protected string Name;

    public event Action HealthDecreased;

    protected bool IsDead => _health.CurrentValue == 0;

    public virtual void Initialize()
    {
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
            Debug.Log($"{Name} is dead");
        }
    }

    protected void DestroyThisObject()
    {
        int miliseconds = (int)_detroyDelay * 1000;
        Task.Delay(miliseconds);
        enabled = false;
        Destroy(gameObject, _detroyDelay + 0.1f);
    }
}
