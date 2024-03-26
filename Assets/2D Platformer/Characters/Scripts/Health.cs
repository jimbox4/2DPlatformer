using System;
using UnityEngine;

[Serializable]
public class Health
{
    [SerializeField] private int _maxValue;
    [SerializeField] private int _currentValue;

    public int CurrentValue => _currentValue;
    public bool IsMaxValue => _currentValue == _maxValue;

    public bool TryIncrease(int value)
    {
        if (value < 0)
        {
            return false;
        }

        _currentValue = Mathf.Clamp(_currentValue + value, 0, _maxValue);

        return true;
    }

    public bool TryDecrease(int value) 
    {
        if (value < 0)
        {
            return false;
        }

        _currentValue = Mathf.Clamp(_currentValue - value, 0, _maxValue);

        return true;
    }
}
