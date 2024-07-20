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
        filter.SteeringPerceiver = perceiver;
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

