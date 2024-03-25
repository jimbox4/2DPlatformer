using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private ChestAnimator _animator;
    [Header("Coins")]
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _coinsCount;
    [SerializeField] private Transform _coinSpawnPoint;
    [SerializeField] private float _coinSpawnDelay;
    [Header("Interact key")]
    [SerializeField] private KeyVisability _key;

    private bool _isOpen = false;

    public void Interact()
    {
        if (_isOpen == false)
        {
            _key.Disable();
            _isOpen = true;
            _animator.Open(_isOpen);

            StartCoroutine(ThrowCoins(_coinSpawnDelay));
        }
    }

    public void ShowKey()
    {
        _key.Show();
    }

    public void HideKey()
    {
        _key.Hide();
    }

    private IEnumerator ThrowCoins(float delay)
    {
        var wait = new WaitForSeconds(delay);

        for (int i = _coinsCount; i > 0; i--)
        {
            Instantiate(_prefab, _coinSpawnPoint.transform);

            yield return wait;
        }
    }
}
