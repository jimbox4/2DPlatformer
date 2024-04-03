using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private Character Character;

    protected Health Health => Character.GetHealth;

    public abstract void Initialize();

    private void OnEnable()
    {
        Character.GetHealth.OnDecreased += UpdateCurrentValue;
        Character.GetHealth.OnIncreased += UpdateCurrentValue;
        Character.GetHealth.OnMaxValueChanged += UpdateMaxValue;
    }

    private void OnDisable()
    {
        Character.GetHealth.OnDecreased -= UpdateCurrentValue;
        Character.GetHealth.OnIncreased -= UpdateCurrentValue;
        Character.GetHealth.OnMaxValueChanged -= UpdateMaxValue;
    }

    protected abstract void UpdateMaxValue();
    protected abstract void UpdateCurrentValue();
}
