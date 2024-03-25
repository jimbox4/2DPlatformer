using System;
using UnityEngine;

[Serializable]
public class Weapon
{
    [SerializeField] private int _damage;

    public int Damage => _damage;
}
