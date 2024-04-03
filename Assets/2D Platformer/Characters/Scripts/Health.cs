using System;
using UnityEngine;

[Serializable]
public class Health
{
    [SerializeField] private int _maxValue;
    [SerializeField] private int _currentValue;

    public int CurrentValue => _currentValue;
    public int MaxValue => _maxValue;
    public bool IsMaxValue => _currentValue == _maxValue;

    public event Action OnDecreased;
    public event Action OnIncreased;
    public event Action OnMaxValueChanged;

    public bool TryIncrease(int value)
    {
        if (value < 0)
        {
            return false;
        }

        _currentValue = Mathf.Clamp(_currentValue + value, 0, _maxValue);

        OnIncreased?.Invoke();

        return true;
    }

    public bool TryDecrease(int value) 
    {
        if (value < 0)
        {
            return false;
        }

        _currentValue = Mathf.Clamp(_currentValue - value, 0, _maxValue);

        OnDecreased?.Invoke();

        return true;
    }

    public void IncreaseMaxValue(int value)
    {
        _maxValue += value;

        OnMaxValueChanged?.Invoke();
    }

    public void DecreaseMaxValue(int value)
    {
        _maxValue -= value;

        if (_maxValue <= 0)
        {
            _maxValue = 0;
        }

        Mathf.Clamp(_currentValue, 0, _maxValue);

        OnMaxValueChanged?.Invoke();
    }
}
