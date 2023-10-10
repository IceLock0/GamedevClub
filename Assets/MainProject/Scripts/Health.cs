using System;
using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100.0f;
    private float _currentHealth;

    public event Action<float> HealthChanged;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ChangeHealth(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Death();
        }
        else
        {
            float _currentHealthPercant = _currentHealth / _maxHealth;
            HealthChanged?.Invoke(_currentHealthPercant);
        }
    }

    private void Death()
    {
        HealthChanged?.Invoke(0);

        gameObject.SetActive(false);
    }
}
