using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;
    [SerializeField]
    private float _maximumHealth;

    public float remainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth; ;
        }
    }
    public void TakeDamage(int bulletDamage)
    {
        if (_currentHealth == 0)
        {
            GameObject.Destroy(gameObject);
        }
        Debug.Log("dano");

        _currentHealth -= bulletDamage;
      

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }
    }
}
