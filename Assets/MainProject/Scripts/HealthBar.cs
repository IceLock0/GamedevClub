using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFillingImage;
    [SerializeField] private Health _health;

    private void Awake()
    {
        _health.HealthChanged += OnHealthChanged;
    }
    private void OnHealthChanged(float valuePercent)
    {
        _healthBarFillingImage.fillAmount = valuePercent;
    }

    private void OnDestroy()
    {
        _health.HealthChanged -= OnHealthChanged;
    }
}
