using Polarith.AI.Move;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask mask;

    private Vector3[] directions = new Vector3[4];
    private Vector3 finalDir;

    [SerializeField] float fireRate = 1f;
    public float fireCountDown;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform body;

    [SerializeField] private int bulletDamage;

    private PlayerManager playerManager;


    public void Setup(PlayerManager manager)
    {
        playerManager = manager;
    }

    private void OnDestroy()
    {
        playerManager.RemovePlayer(this);
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(body.position, sensorRange, mask);

        Collider2D closest = null;
        float minDistance = 999;

        for (int i = 0; i < colliders.Length; i++)
        {
            var distance = Vector3.Distance(colliders[i].transform.position, body.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = colliders[i];
            }

        }
        if (closest != null)
        {
            var direction = closest.transform.position - body.position;

            body.up = direction;

            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

        }

        fireCountDown -= Time.deltaTime;
    }
    public void Shoot()

    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.Set(bulletDamage);
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

    public void OnPointerClick(PointerEventData eventData)
    {

    }
}

