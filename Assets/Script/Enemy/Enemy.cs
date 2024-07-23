using Polarith.AI.Move;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private AIMSteeringFilter filter;
    [SerializeField]
    private AIMSimpleController2D controller;

    private float stunTime = 0;
    public bool Stunned;

    public void Setup(AIMSteeringPerceiver perceiver)
    {
        //Debug.Log(filter.SteeringPerceiver);
        //Debug.Log(perceiver);
        filter.SteeringPerceiver = perceiver;
        //Debug.Log(filter.SteeringPerceiver);
    }


    public void Stun(float stunTime)
    {
        if (this.stunTime < stunTime)
        {
            Stunned = true;
            controller.enabled = false;
            this.stunTime = stunTime;
        }
    }

    private void Update()
    {
        if (!Stunned)
        {
            return;
        }

        stunTime -= Time.deltaTime;
        if (stunTime <= 0)
        {
            controller.enabled = true;
            Stunned = false;
        }
    }
}

