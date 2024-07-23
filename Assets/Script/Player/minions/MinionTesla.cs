using UnityEngine;

public class MinionTesla : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private int teslaDamage;
    [SerializeField] private Transform body;

    [SerializeField] float fireRate = 1f;
    public float fireCountDown;

    [SerializeField] private Transform firePoint;

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
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (fireCountDown <= 0f)
        {
            if (collision.tag == "Inimigo")
            {
                var enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(teslaDamage);
                Debug.Log("danoTesla");

            }
        }
        
    }

}




