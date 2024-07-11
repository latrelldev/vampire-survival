using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorInimigo : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject playerRef;

    [SerializeField]
    private float minimumSpawnTime;
    [SerializeField]
    private float maxSpawnTime;
    private float timeUntilSpawn;

    public float xLimit;
    public float spawnRate;

    private void Start()
    {
        StartSpawning();
    }
    void Awake()
    {
        SetTimeUntilSpawn();  
    }

   
    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maxSpawnTime);
    }

    void StartSpawning()
    {

        InvokeRepeating("Spawn", 1f, spawnRate);
    }

    void Spawn()
    {
        float randomX = Random.Range(-xLimit, xLimit);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);

        var enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.GetComponent<InimigMovimentação>().SetTarget(playerRef);
        SetTimeUntilSpawn();
    }

    public void stopSpawn()
    {
        CancelInvoke("spawn");
    }
}
