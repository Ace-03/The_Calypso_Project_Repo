using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<WaveDefinitionSO> waveSequence;
    public Transform playerTransform;

    private int currentWaveIndex = 0;
    private float waveTimer = 0f;

    private void Start()
    {
        InitializeEnemyPools();

        waveTimer = waveSequence[0].waveDuration;
        StartCoroutine(SpawnEnemies());
    }

    private void InitializeEnemyPools()
    {
        HashSet<GameObject> uniqueEnemyPrefabs = new HashSet<GameObject>();
        
        foreach (var wave in waveSequence)
        {
            foreach (var enemySpawnInfo in wave.enemiesInWave)
            {
                uniqueEnemyPrefabs.Add(enemySpawnInfo.enemyDefinition.enemyPrefab);
            }
        }

        foreach (var enemyPrefab in uniqueEnemyPrefabs)
        {
            PoolManager.Instance.CreatePool(enemyPrefab.name, enemyPrefab, 25);
        }
    }

    private void Update()
    {
        if (waveTimer <= 0)
        {
            currentWaveIndex++;
            if (currentWaveIndex < waveSequence.Count)
            {
                waveTimer = waveSequence[currentWaveIndex].waveDuration;
                StartCoroutine(SpawnEnemies());
            }
            else
            {
                Debug.Log("All waves completed!");
            }
        }
        waveTimer -= Time.deltaTime;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            WaveDefinitionSO currentWave = waveSequence[currentWaveIndex];

            foreach (var enemySpawnInfo in currentWave.enemiesInWave)
            {
                float spawnInterval = 1f / enemySpawnInfo.spawnRate;

                if (GameObject.FindGameObjectsWithTag("Enemy").Length < enemySpawnInfo.maxActiveEnemies)
                {
                    SpawnEnemy(enemySpawnInfo.enemyDefinition);
                }

                yield return new WaitForSeconds(spawnInterval);
            }   
        }
    }

    private void SpawnEnemy(EnemyDefinitionSO enemyType)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject enemyInstance =  PoolManager.Instance.GetFromPool(enemyType.name, spawnPosition, Quaternion.identity);

        if (enemyInstance == null )
        {
            Debug.LogError($"Failed to spawn enemy of type {enemyType.name}");
            return;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return playerTransform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-10, 10));
    }
}
