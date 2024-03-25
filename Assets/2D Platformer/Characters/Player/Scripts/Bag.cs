using System;
using UnityEngine;

[Serializable]
public class Bag
{
    [SerializeField] private int _coins;

    public void IncreaseCoins(int value)
    {
        if (value < 0)
        {
            return;
        }

        _coins += value;
    }

    public void DecreaseCoins(int value) 
    {
        if (value < 0)
        {
            return;
        }

        _coins -= value;

        if (_coins < 0)
        {
            _coins = 0;
        }
    }
}
