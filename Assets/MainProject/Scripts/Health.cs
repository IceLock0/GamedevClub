using System;
using System.Collections;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100.0f;
    public float currentHealth { get; private set; } 

    public event Action<float> HealthChanged;

    private void Start()
    {
        currentHealth = _maxHealth;
    }

    public void ChangeHealth(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            float _currentHealthPercant = currentHealth / _maxHealth;
            HealthChanged?.Invoke(_currentHealthPercant);
        }
    }

    private void Death()
    {
        HealthChanged?.Invoke(0);

        gameObject.SetActive(false);
    }
}
