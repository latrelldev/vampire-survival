using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigMovimentação : MonoBehaviour
{
    public GameObject player;
    public float speed;


    private float distance;

    public void SetTarget(GameObject target)
    {
        player = target;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, transform.forward);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bala")
        {
            gameObject.SetActive(false);
        }

        if (collision.tag == "Player")
        {

            var healthComponent = collision.GetComponent<Health>();
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
            }
        }
    }
}
