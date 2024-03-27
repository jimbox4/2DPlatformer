using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(SpellReturnToPool))]
public class DeathBringerSpell : MonoBehaviour
{
    [Header("Damage zone")]
    [SerializeField] private BoxCollider2D _damageZone;
    [Header("Ground layer")]
    [SerializeField] private LayerMask _groundLayerMask;
    [Header("Animator script")]
    [SerializeField] private SpellAnimator _animator;

    private int _damage;
    private SpellReturnToPool _spellReturnToPool;

    private void Awake()
    {
        _spellReturnToPool = GetComponent<SpellReturnToPool>();
    }

    private void OnEnable()
    {
        _animator.HandAttack += StartSpellCoroutine;
        _animator.HandStopAttack += StopSpellCoroutine;
        _animator.EndAnimation += Reliaze;

        SetStartPosition();
    }

    private void OnDisable()
    {
        _animator.HandAttack -= StartSpellCoroutine;
        _animator.HandStopAttack -= StopSpellCoroutine;
        _animator.EndAnimation -= Reliaze;
    }

    public void Initialize(int damage)
    {
        _damage = damage;
    }

    private void SetStartPosition()
    {
        RaycastHit2D groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1000, _groundLayerMask);

        transform.position = new Vector2(transform.position.x, transform.position.y - groundHit.distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }

    private void Reliaze()
    {
        _spellReturnToPool.Release();
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
