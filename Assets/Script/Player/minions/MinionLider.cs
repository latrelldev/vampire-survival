using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLider :MonoBehaviour
{
    [SerializeField] private GameObject minionLider;
    private Player damage;
    private PointGo speedMinions;
   


    void Start()
    {
        speedMinions = GetComponent<PointGo>();
        damage = GetComponent<Player>();
    }

    void Update()
    {
        
        if (minionLider != null)
        {
            speedMinions.speed = speedMinions.speed * 2;
            damage.bulletDamage = damage.bulletDamage * 2;
        }

        else
        {
            return;
        }

        if (speedMinions.speed == speedMinions.speed * 2)
        {
            return;
        }

        if (damage.bulletDamage == damage.bulletDamage * 2)
        {
            return;
        }
    }

    
}
