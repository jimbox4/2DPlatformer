using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DeathBringer _deathBringer;

    private void Awake()
    {
        _player.Initialize();
        _deathBringer.Initialize();
    }
}
