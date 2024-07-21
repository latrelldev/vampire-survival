using Polarith.AI.Move;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform holder;
    public List<Enemy> Enemies = new List<Enemy>();

    [SerializeField] private AIMSteeringPerceiver perceiver;

    private void Start()
    {
        Enemies = GetComponentsInChildren<Enemy>().ToList();
        foreach (Enemy enemy in Enemies)
        {
            enemy.Setup(perceiver);
        }
    }

    public Enemy SpawnEnemy(Enemy enemyPrefab, Vector3 position)
    {
        Enemy enemy = Instantiate(enemyPrefab, position, Quaternion.identity, holder);
        enemy.Setup(perceiver);
        Enemies.Add(enemy);
        return enemy;
    }

    public void DestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        Enemies.Remove(enemy);
    }
}

