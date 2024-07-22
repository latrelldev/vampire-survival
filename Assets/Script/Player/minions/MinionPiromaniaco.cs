using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPiromaniaco : MonoBehaviour
{
    [SerializeField] private float sensorRange = 5f;

    private Vector3[] directions = new Vector3[4];
    private Vector3 finalDir;

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
