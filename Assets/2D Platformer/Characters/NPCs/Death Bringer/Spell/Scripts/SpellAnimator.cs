using System;
using UnityEngine;

public class SpellAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action HandAttack;
    public event Action HandStopAttack;
    public event Action EndAnimation;

    public void ShowHand()
    {
        HandAttack?.Invoke();
    }

    public void HideHand()
    {
        HandStopAttack?.Invoke();
    }

    public void EndSpellAnimation()
    {
        EndAnimation?.Invoke();
    }
}
