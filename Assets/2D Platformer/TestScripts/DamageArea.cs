using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In trigger");

        if(collision.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }
}
