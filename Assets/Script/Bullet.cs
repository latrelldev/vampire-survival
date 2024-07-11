using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject inimigo;
    private Rigidbody2D rb;
    public float bulletForce = 20f;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (inimigo == null)
        {
            
            inimigo = GameObject.FindGameObjectWithTag("Inimigo");
            Vector3 direction = inimigo.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;

        }
        timer += Time.deltaTime;
        if(timer > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            Destroy(gameObject);
        }
    }
}
