using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Enemy enemyPrefab;
    public List<Enemy> Enemies = new List<Enemy>();

    private void Start()
    {
        Enemies = GetComponentsInChildren<Enemy>().ToList();
        foreach (Enemy enemy in Enemies)
        {
            enemy.Setup(gameManager.Player);
        }
    }

    public Enemy SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab);
        enemy.Setup(gameManager.Player);
        Enemies.Add(enemy);
        return enemy;
    }

    public void DestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        Enemies.Remove(enemy);
    }
}

