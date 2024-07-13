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


    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sensorRange, mask);

        directions = new Vector3[colliders.Length];
        finalDir = Vector3.zero;

        for (int i = 0; i < colliders.Length; i++)
        {
            var direction =   colliders[i].transform.position - transform.position;
            var totalDirection = direction.normalized * sensorRange;
            directions[i] = -totalDirection + direction;
            finalDir += directions[i];
        }

        transform.position += finalDir.normalized * Time.deltaTime * moveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
        foreach(var direction in directions)
        {
            finalDir += direction;
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + finalDir);
    }
}

