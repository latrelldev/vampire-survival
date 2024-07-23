using Polarith.AI.Move;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMedico : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float sensorRange = 5f;
    [SerializeField] private Transform body;

    public float fireCountDown;
    [SerializeField] float enemyCountDown;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRadius;
    [SerializeField] private float fireDistance;
    [SerializeField] private int lifeBuff;

    [SerializeField] private Player player;

    private List<Player> buffedPlayers = new List<Player>();

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

      
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sensorRange);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trying to buff life");
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && !buffedPlayers.Contains(player))
        {
            Debug.Log("buffing life");
            buffedPlayers.Add(player);
            player.LifeModifier += lifeBuff;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trying to debuff life");
        var player = collision.gameObject.GetComponent<Player>();
        if (player != null && buffedPlayers.Contains(player))
        {
            Debug.Log("Debuff life");
            buffedPlayers.Remove(player);
            player.LifeModifier += lifeBuff;
        }
    }
}
