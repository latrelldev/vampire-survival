using Polarith.AI.Move;
using UnityEngine;

public class MinionTesla : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private int teslaDamage;
    [SerializeField] private Transform body;

    [SerializeField] float fireRate = 1f;
    public float fireCountDown;
    [SerializeField] float enemyCountDown;

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
        enemyCountDown -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (fireCountDown <= 0f)
        {
            //pegar posicao do tiro -> posicao do player + (direcao que ele ta olhando * distancia do player)
            var point = transform.position + (body.up * fireDistance);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point, fireRadius, mask);
            foreach(var collider in colliders)
            {
                var enemy = collider.gameObject.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamager(teslaDamage);
                    Debug.Log("Dano Tesla");
                   
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        var enemy = GetComponent<Collider>().gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (enemyCountDown == 4f)
            {
                GetComponent<AIMSteeringPerceiver>().gameObject.SetActive(false);
            }

            if (enemyCountDown <= 0f)
            {
                GetComponent<AIMSteeringPerceiver>().gameObject.SetActive(true);
            }
        }

    }

}




