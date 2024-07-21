using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private PointGo player;
    private Animator animator;
    private Player shoot;

    void Start()
    {
        player = GetComponent<PointGo>();
        animator = GetComponent<Animator>();
        shoot = GetComponent<Player>();
    }

    void Update()
    {
        if (player.moving == true)
        {
            animator.SetInteger("Transition", 1);
            
        }

        else
        {
            animator.SetInteger("Transition", 0);
            
        }

        if (shoot.fireCountDown <= 0f) 
        {
            animator.SetInteger("Transition", 2);
           
        }
    }
}
