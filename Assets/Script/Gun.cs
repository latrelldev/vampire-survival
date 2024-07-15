using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
   
    private Transform target;

    [Header("Atributes")]
    public float fireRate = 0.5f;
    private float fireCountDown = 0f;
    public float range = 15f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform rotate;
    public float turnSpeed;

    public GameObject bulletPrefab
    

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range )
        {
          target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if(target == null )
        {
            return;
        }

        Vector3 dir = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotate.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;
        rotate.rotation = Quaternion.Euler(0f,rotation.y,0f);//lookRotation usar para todos os angulos 

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f /fireRate;
        }

        fireCountDown -= Time.deltaTime;

    }

    private void Shoot()
    {
        Debug.Log("shoot");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }


}
