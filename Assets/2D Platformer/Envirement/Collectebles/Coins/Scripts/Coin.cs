using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Coin : MonoBehaviour, ICollecteble
{
    [SerializeField, Min(0)] private int _value;
    [SerializeField, Min(0)] private int _force;

    private void Start()
    {
        int forceMultiplier = 10;
        float minVectorRange = -0.4f;
        float maxVectorRange = 0.4f;

        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        Vector2 throwDirection = new Vector2 (Random.Range(minVectorRange, maxVectorRange), 1).normalized;

        rigidbody.AddForce(throwDirection * _force * forceMultiplier);
    }

    public int Collect()
    {
        Destroy(gameObject);

        return _value;
    }
}
