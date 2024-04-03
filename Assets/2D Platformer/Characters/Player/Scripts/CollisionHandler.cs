using System;
using UnityEngine;

[Serializable]
public class CollisionHandler
{
    [field:SerializeField] public LayerMask CollectebleLayerMask { get; private set; }

    public bool TryHandleCoinCollision(Collision2D collision, out int coinPoints)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            coinPoints = coin.Collect();
            return true;
        }

        coinPoints = 0;

        return false;
    }

    public bool TryHandleHeartCollision(Collision2D collision, out int healPoints)
    {
        if (collision.gameObject.TryGetComponent(out Heart heart))
        {
            healPoints = heart.Collect();

            return true;
        }

        healPoints = 0;

        return false;
    }
}
