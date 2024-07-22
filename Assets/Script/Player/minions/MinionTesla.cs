using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionTesla : MonoBehaviour
{
    [SerializeField] private int teslaDamage;
    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private LayerMask mask;

    private Vector3[] directions = new Vector3[4];
    private Vector3 finalDir;

    private EnemyHealth enemy;

    [SerializeField] private Transform body;

    private void Start()
    {
        enemy = GetComponent<EnemyHealth>();
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
            var enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(teslaDamage);
            Debug.Log("danoInimigo");

        }
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
