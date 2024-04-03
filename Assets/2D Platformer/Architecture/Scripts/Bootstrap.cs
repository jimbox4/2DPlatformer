using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DeathBringer _deathBringer;
    [SerializeField] private Bar[] _Bars;

    private void Awake()
    {
        _player.Initialize();
        _deathBringer.Initialize();
        
        foreach (var bar in _Bars)
        {
            bar.Initialize();
        }
    }
}
