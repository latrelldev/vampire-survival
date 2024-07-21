using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maximumHealth;
    public Slider HealthBar;

    public void Start()
    {
        HealthBar.value = _currentHealth;  
    }

    public float remainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth; ;
        }
    }

    

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        HealthBar.value -= damageAmount;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        if(_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }
}
