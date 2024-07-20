using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player target;
    [SerializeField] private float movespeed = 1f;

    public void Setup(Player target)
    {
        this.target = target;
    }

    private void Update()
    {
        var direction = target.transform.position - transform.position;
        transform.position += movespeed * Time.deltaTime * direction.normalized;
    }
}

