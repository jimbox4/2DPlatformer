using UnityEngine;
using UnityEngine.UI;

public class VampirizmSkillBar : MonoBehaviour
{
    private const int MaxFill = 1;

    [SerializeField] private Image _image;

    private float _maxValue;

    public void Initialize(float maxValue)
    {
        _maxValue = maxValue;
    }

    public void UpdateValue(float currentValue)
    {
        _image.fillAmount = MaxFill / _maxValue * currentValue;
    }

    public void SetMaxValue()
    {
        _image.fillAmount = MaxFill;
    }
}
