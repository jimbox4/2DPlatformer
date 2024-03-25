using System;
using UnityEngine;

[Serializable]
public class Health
{
    [SerializeField] private int _maxValue;
    [SerializeField] private int _currentValue;

    public int CurrentValue => _currentValue;

    public bool TryIncrease(int value)
    {
        if (value < 0)
        {
            return false;
        }

        _currentValue += value;

        if (_currentValue > _maxValue)
        {
            _currentValue = _maxValue;
        }

        return true;
    }

    public bool TryDecrease(int value) 
    {
        if (value < 0)
        {
            return false;
        }

        _currentValue -= value;

        if (_currentValue < 0)
        {
            _currentValue = 0;
        }

        return true;
    }
}
