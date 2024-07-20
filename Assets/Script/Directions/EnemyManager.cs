using Polarith.AI.Move;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Enemy enemyPrefab;
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

    public Enemy SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
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

