using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAtack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
         var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

         playerHealth.TakeDamage(_damageAmount);
            Debug.Log("dano");
        }
    }
}
