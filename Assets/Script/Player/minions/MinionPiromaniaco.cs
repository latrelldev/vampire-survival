using Polarith.AI.Move;
using System.Linq;
using UnityEngine;

public class MinionPiromaniaco : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private int fireDamage;
    [SerializeField] private Transform body;

    [SerializeField] float fireRate = 1f;
    public float fireCountDown;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRadius;
    [SerializeField] private float fireDistance;

    [SerializeField] private Player player;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sensorRange, mask);

        Collider2D closest = null;
        float minDistance = 999;

        for (int i = 0; i < colliders.Length; i++)
        {
            var distance = Vector3.Distance(colliders[i].transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = colliders[i];
            }

        }
        if (closest != null)
        {
            var direction = closest.transform.position - transform.position;
            body.up = direction;
        }

        fireCountDown -= Time.deltaTime;

        var point = transform.position + (body.up * (fireDistance + fireRadius));
        colliders = Physics2D.OverlapCircleAll(point, fireRadius, mask);
        bool hasTarget = false;

        if (fireCountDown <= 0)
        {
            foreach (var collider in colliders)
            {
                var enemy = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    hasTarget = true;
                    enemy.TakeDamage(fireDamage + player.PowerModifier);
                    Debug.Log("Dano Fire " + (fireDamage + player.PowerModifier));
                    fireCountDown = 1f / fireRate;
                }
            }
        }
        //animacao = hasTarget
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (body.up * (fireDistance + fireRadius)), fireRadius);
    }
}




