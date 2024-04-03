using System.Threading.Tasks;
using UnityEngine;

[SelectionBase]
public abstract class Character : MonoBehaviour
{
    [SerializeField, Min(0)] private float _detroyDelay;
    [Header("Stats")]
    [SerializeField] protected Health Health;

    protected string Name;

    public Health GetHealth => Health;
    public bool IsDead => Health.CurrentValue == 0;
    protected bool HasMaxHealth => Health.IsMaxValue;

    public virtual void Initialize()
    {
        Name = gameObject.name;
    }

    public void Heal(int healthPoints)
    {
        if (Health.TryIncrease(healthPoints))
        {
            Debug.Log($"{Name} take heal {healthPoints}");
        }
    }

    public void TakeDamage(int damage)
    {
        if (Health.TryDecrease(damage) == false || IsDead == true)
        {
            return;
        }

        Debug.Log($"{Name} take {damage} damage");

        if (Health.CurrentValue == 0)
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
        enabled = false;
    }
}
