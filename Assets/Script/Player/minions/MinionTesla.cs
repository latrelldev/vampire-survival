using Polarith.AI.Move;
using UnityEngine;

public class MinionTesla : MonoBehaviour
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
    [SerializeField] private float stunTime;

    [SerializeField] private Player player;
    private bool hasTarget = false;

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
        

        if (fireCountDown <= 0)
        {
            foreach (var collider in colliders)
            {
                var enemy = collider.gameObject.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    hasTarget = true;
                    enemy.TakeDamage(fireDamage + player.PowerModifier);
                    Debug.Log("Dano Tesla" + (fireDamage + player.PowerModifier));
                    fireCountDown = 1f / fireRate;
                    enemy.GetComponent<Enemy>().Stun(stunTime);
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

