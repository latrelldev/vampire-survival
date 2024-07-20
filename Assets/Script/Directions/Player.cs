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

    [Range(0f, 1f)]
    public float steeringStrength = 1f;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sensorRange, mask);

        directions = new Vector3[colliders.Length];

        Vector3 avoidances = Vector3.zero;

        for (int i = 0; i < colliders.Length; i++)
        {
            var distance = Mathf.Clamp(Vector3.Distance(colliders[i].transform.position, transform.position), 0, sensorRange);
            distance = sensorRange - distance;
            var direction = colliders[i].transform.position - transform.position;

            //var totalDirection = direction.normalized * sensorRange;


            //directions[i] = -totalDirection + direction;
            directions[i] = -direction.normalized * distance;
            avoidances += directions[i];
            //finalDir += directions[i];
        }

        avoidances = avoidances.normalized;
        float avoidanceStrength = 1 - steeringStrength;
        finalDir = ((finalDir * steeringStrength) + (avoidances * avoidanceStrength)).normalized;
        transform.position += moveSpeed * Time.deltaTime * finalDir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
        Gizmos.color = Color.yellow;
        foreach (var direction in directions)
        {
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + finalDir);
    }
}

