using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Couting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Enemy enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    [SerializeField] private EnemyManager enemyManager;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private float searchCountDown = 1f;

    private SpawnState state = SpawnState.Couting;

    void Start()
    {
        waveCountDown = timeBetweenWaves;

    }

    void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (EnemyIsAlive() == false)
            {
                WaveCompleted();
            }

            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }

        }

        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("completa wave");

        state = SpawnState.Couting;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("waves completas");
        }

        else
        {
            nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Inimigo") == null)
            {
                return false;
            }
        }
        return true;
    }


    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave:" + _wave.name);

        state = SpawnState.Spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            enemyManager.SpawnEnemy(_wave.enemy, transform.position);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.Waiting;

        yield break;
    }
}
