using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeAtirar : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireSpeed = 1;
    private float timer;
    public GameObject inimigo;
    public float minDistance;
    public float bulletForce = 20f;

    void Update()
    {
        if (inimigo == null)
        {
            inimigo = GameObject.FindGameObjectWithTag("Inimigo");
        }

        if (inimigo == null)
        {
            return;
        }




        float distance = Vector2.Distance(transform.position, inimigo.transform.position);


        if (distance < minDistance)
        {
            timer += Time.deltaTime;
            if (timer > fireSpeed)
            {
                timer = 0;
                Shoot();
            }
        }
    }


    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);

    }
}
