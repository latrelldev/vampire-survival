using Polarith.AI.Move;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 5;
    [SerializeField]
    private AIMSteeringFilter filter;

    public void Setup(AIMSteeringPerceiver perceiver)
    {
        //Debug.Log(filter.SteeringPerceiver);
        //Debug.Log(perceiver);
        filter.SteeringPerceiver = perceiver;
        //Debug.Log(filter.SteeringPerceiver);
    }

    public void TakeDamager(int value)
    {
        health -= value;
        if (health < 0)
        {
            Destroy(gameObject);
        }

    }
}

