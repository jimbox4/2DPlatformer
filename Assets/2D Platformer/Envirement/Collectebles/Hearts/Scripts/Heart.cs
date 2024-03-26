using UnityEngine;

public class Heart : MonoBehaviour, ICollecteble
{
    [SerializeField] private int _healthPoints;

    public int Collect()
    {
        Destroy(gameObject);

        return _healthPoints;
    }
}
