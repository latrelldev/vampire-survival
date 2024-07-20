using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask mask;

    private Vector3[] directions = new Vector3[4];
    private Vector3 finalDir;

    public float fireRate = 1f;
    private float fireCountDown;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public int bulletDamage;

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

            transform.up = direction;

            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }
           
        }
   
        fireCountDown -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
        foreach (var direction in directions)
        {
            finalDir += direction;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + finalDir);
    }
}

 