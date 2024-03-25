using UnityEngine;

[SelectionBase]
public class DeathBringerSpell : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int _damage;
    [Header("Damage zone")]
    [SerializeField] private BoxCollider2D _damageZone;
    [Header("Animator script")]
    [SerializeField] private SpellAnimator _animator;

    private void OnEnable()
    {
        _animator.HandAttack += StartSpellCoroutine;
        _animator.HandStopAttack += StopSpellCoroutine;
        _animator.EndAnimation += Destroy;
    }

    private void OnDisable()
    {
        _animator.HandAttack -= StartSpellCoroutine;
        _animator.HandStopAttack -= StopSpellCoroutine;
        _animator.EndAnimation -= Destroy;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }
    
    private void Destroy()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }

    private void StartSpellCoroutine()
    {
        _damageZone.enabled = true;
    }

    private void StopSpellCoroutine()
    {
        _damageZone.enabled = false;
    }
}
