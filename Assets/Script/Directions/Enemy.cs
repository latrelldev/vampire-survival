using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player target;

    public void TakeDamager(int value)
    {
        Destroy(gameObject);
    }

    public void Setup(Player target)
    {
        this.target = target;
    }
}

