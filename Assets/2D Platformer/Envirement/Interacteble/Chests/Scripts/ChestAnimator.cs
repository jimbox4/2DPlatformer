using UnityEngine;

[SelectionBase]
public class ChestAnimator : MonoBehaviour
{
    private readonly int IsOpen = Animator.StringToHash(nameof(IsOpen));
    [SerializeField] private Animator _animator;

    public void Open(bool isOpen)
    {
        _animator.SetBool(IsOpen, isOpen);
    }
}
