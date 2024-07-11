using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeAtirar : MonoBehaviour
{
  public Transform firePoint;
  public GameObject bulletPrefab;

    private float timer;
    public GameObject inimigo;

  public float bulletForce = 20f;

   


    void Update()
    {
        if(inimigo == null)
        {
            inimigo = GameObject.FindGameObjectWithTag("Inimigo");
        }

        float distance = Vector2.Distance(transform.position, inimigo.transform.position);
       

        if (distance < 5)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                Shoot();
            }
        }      
    }
  

  void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
      
    }
}
