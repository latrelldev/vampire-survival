using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player target;

    public void Setup(Player target)
    {
        this.target = target;
    }
}

