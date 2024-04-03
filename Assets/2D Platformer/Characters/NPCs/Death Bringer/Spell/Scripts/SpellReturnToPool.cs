using UnityEngine;
using UnityEngine.Pool;

public class SpellReturnToPool : MonoBehaviour
{
    private IObjectPool<DeathBringerSpell> _pool;
    private DeathBringerSpell _spell;

    public void Initialize(IObjectPool<DeathBringerSpell> pool, DeathBringerSpell spell)
    {
        _pool = pool;
        _spell = spell;
    }

    public void Release()
    {
        _pool.Release(_spell);
    }
}
