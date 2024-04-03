using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class KeyVisability : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Color _full = new Color(1, 1, 1, 1);
    private Color _invisible = new Color(1, 1, 1, 0);

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        _spriteRenderer.color = _full;
    }

    public void Hide()
    {
        _spriteRenderer.color = _invisible;
    }
}
