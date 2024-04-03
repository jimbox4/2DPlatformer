using System;
using UnityEngine;

[Serializable]
public class InteractSystem
{
    [SerializeField] private BoxCollider2D _area;
    [SerializeField] private LayerMask _layerMask;

    private Transform _transform;

    public void Initialize(Transform transform)
    {
        _transform = transform;
    }

    public bool TryShowInteractKey(Collider2D collision)
    {
        if (collision != null && collision.TryGetComponent(out IInteractable interactable))
        {
            interactable.ShowKey();
            return true;
        }

        return false;
    }

    public void HideInteractKey(Collider2D collision)
    {
        if (collision != null && collision.TryGetComponent(out IInteractable interactable))
        {
            interactable.HideKey();
        }
    }

    public void Interact()
    {
        Collider2D nearest = null;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_area.transform.position, _area.size, 0, _layerMask);

        if (colliders.Length == 0) 
        { 
            return;
        }

        float minDistance = Vector2.Distance(colliders[0].transform.position, _transform.position);
        nearest = colliders[0];
        float distance;

        foreach (var collider in colliders)
        {
            distance = Vector2.Distance(collider.transform.position, _transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = collider;
            }
        }

        if (nearest.TryGetComponent(out IInteractable interacteble))
        {
            interacteble.Interact();
        }
    }
}
