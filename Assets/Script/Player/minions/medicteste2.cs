using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class MedicoTeste2 : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private Transform body;

    public float fireCountDown;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRadius;
    [SerializeField] private float fireDistance;
    [SerializeField] private float lifeAmount;

    [SerializeField] private Player player;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sensorRange, mask);

        Collider2D closest = null;
        float minDistance = 999;
        fireCountDown -= Time.deltaTime;

        for (int i = 0; i < colliders.Length; i++)
        {
            var distance = Vector3.Distance(colliders[i].transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = colliders[i];
            }
        }
        
        if(fireCountDown<=0)
        {
            foreach (var collider in colliders)
            {
                var player = collider.gameObject.GetComponent<PlayerHealth>();
                if (closest != null)
                {
                    var direction = closest.transform.position - transform.position;
                    body.up = direction;
                    player.AddHealth(lifeAmount);
                }
            }
        }
        //bool hasTarget = false;

        //animacao = hasTarget
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (body.up * (fireDistance + fireRadius)), fireRadius);
    }
}
