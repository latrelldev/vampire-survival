using Polarith.AI.Move;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator animator;
    private GameObject enemy;
    void Start()
    {
       animator = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Inimigo");
    }

    void Update()
    {
        if (enemy.GetComponent<AIMSeek>() == true)
        {
            animator.SetInteger("Transition", 1);
        }

        else
        {
            animator.SetInteger("Transition", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            animator.SetInteger("Transition", 2);
            Debug.Log("anm shoot");
        }
    }
}
