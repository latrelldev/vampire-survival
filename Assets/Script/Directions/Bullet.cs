using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletForce = 20f;
    private float timer;
    private int bulletDamage;

    public void Set(int damage)
    {
        bulletDamage = damage;
    }

    private void Update()
    {

        transform.position += transform.up * bulletForce * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > 5)
        {
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Inimigo")
        {
            var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(bulletDamage);
            Debug.Log("danoInimigo");
        }
    }
}
