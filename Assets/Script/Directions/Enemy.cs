using Polarith.AI.Move;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player target;
    [SerializeField]
    private int health = 5;

    public void TakeDamager(int value)
    {
        health -= value;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    
    }
    private void Awake()
    {
        var filter = GetComponent<AIMSteeringFilter>();
        filter.SteeringPerceiver = FindObjectOfType<AIMSteeringPerceiver>();
    }
    public void Setup(Player target)
    {
        this.target = target;
    }
}

