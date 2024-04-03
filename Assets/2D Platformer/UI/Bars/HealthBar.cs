using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] private Character _character;

    protected Health Health => _character.GetHealth;

    public abstract void Initialize();

    private void OnEnable()
    {
        _character.GetHealth.OnDecreased += UpdateCurrentValue;
        _character.GetHealth.OnIncreased += UpdateCurrentValue;
        _character.GetHealth.OnMaxValueChanged += UpdateMaxValue;
    }

    private void OnDisable()
    {
        _character.GetHealth.OnDecreased -= UpdateCurrentValue;
        _character.GetHealth.OnIncreased -= UpdateCurrentValue;
        _character.GetHealth.OnMaxValueChanged -= UpdateMaxValue;
    }

    protected abstract void UpdateMaxValue();
    protected abstract void UpdateCurrentValue();
}
