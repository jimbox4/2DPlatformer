using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class VampireSkill
{
    [SerializeField] private VampirizmSkillBar _bar;
    [SerializeField] private Transform _center;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField, Min(0)] private float _radius;
    [SerializeField, Min(0)] private int _healthPerTick;
    [SerializeField, Min(0)] private int _replenishTime;
    [SerializeField, Min(0)] private float _duration;
    [SerializeField, Min(0)] private float _tickDelay;

    public event Action OnSkillEnded;

    private bool _enabled = false;
    private Character _character;
    
    public bool CanActivate => _enabled == false;

    public void Initialize(Character character)
    {
        _character = character;
        _bar.Initialize(_duration);
        Start();
    }

    public void Start()
    {
        _particleSystem.gameObject.SetActive(true);
        _bar.enabled = true;
    }

    public void Stop()
    {
        _particleSystem.gameObject.SetActive(false);
        _bar.enabled = false;
    }

    public IEnumerator Vampirize()
    {
        _enabled = true;
        _particleSystem.Play();
        var wait = new WaitForSeconds(_tickDelay);
        float endTime = Time.time + _duration;

        while (Time.time <= endTime)
        {
            TakeHealth();
            _bar.UpdateValue(endTime - Time.time);

            yield return wait;
        }

        _particleSystem.Stop();
        OnSkillEnded?.Invoke();
    }

    public IEnumerator ReplenishAbility()
    {
        float endTime = Time.time + _replenishTime;

        while (Time.time <= endTime)
        {
            _bar.UpdateValue(_replenishTime - (endTime - Time.time));

            yield return null;
        }

        _bar.SetMaxValue();
        _enabled = false;
    }

    private void TakeHealth()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_center.position, _radius, _layerMask);

        if (colliders == null)
        {
            return;
        }

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Character enemy) && enemy.IsDead == false)
            {
                enemy.TakeDamage(_healthPerTick);
                _character.Heal(_healthPerTick);
            }
        }
    }

    public void DrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_center.position, _radius);
    }
}
