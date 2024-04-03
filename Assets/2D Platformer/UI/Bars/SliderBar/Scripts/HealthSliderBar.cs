using UnityEngine;
using UnityEngine.UI;

public class HealthSliderBar : HealthBar
{
    [SerializeField] private Image[] _images;

    private int _maxValue;
    private int _currentValue;
    private float _maxFillValue = 1;

    public override void Initialize()
    {
        UpdateBar();
    }

    protected override void UpdateCurrentValue()
    {
        UpdateBar();
    }

    protected override void UpdateMaxValue()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        _maxValue = Health.MaxValue;
        _currentValue = Health.CurrentValue;

        float fillPercent = _maxFillValue / _maxValue * _currentValue;

        foreach (var image in _images)
        {
            image.fillAmount = fillPercent;
        }
    }
}
