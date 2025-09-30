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

    private void Awake()
    {
        InitializeEnemyPools();

        waveTimer = waveSequence[0].waveDuration;
        StartCoroutine(SpawnEnemies());
    }

    private void InitializeEnemyPools()
    {
        HashSet<EnemyDefinitionSO> uniqueEnemies = new HashSet<EnemyDefinitionSO>();
        
        foreach (var wave in waveSequence)
        {
            foreach (var enemySpawnInfo in wave.enemiesInWave)
            {
                uniqueEnemies.Add(enemySpawnInfo.enemyDefinition);
            }
        }

        foreach (var enemyData in uniqueEnemies)
        {
            PoolManager.Instance.CreatePool(enemyData.name, enemyData.enemyPrefab, 50);
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

                float totalEnemies = FindObjectsByType<EnemyInitializer>(FindObjectsSortMode.None).Length;

                foreach (var enemy in FindObjectsByType<EnemyInitializer>(FindObjectsSortMode.None))
                {
                    if (enemy.GetEnemyData().enemyName != enemySpawnInfo.enemyDefinition.enemyName)
                    {
                        totalEnemies--;
                    }
                }

                if (totalEnemies < enemySpawnInfo.maxActiveEnemies)
                {
                    SpawnEnemy(enemySpawnInfo.enemyDefinition);
                }
                else
                {
                    //Debug.Log($"Max active enemies reached for {enemySpawnInfo.enemyDefinition.name}");
                }

                yield return new WaitForSeconds(spawnInterval);
            }   
        }
    }

    private void SpawnEnemy(EnemyDefinitionSO enemyType)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject enemyInstance =  PoolManager.Instance.GetFromPool(enemyType.name, spawnPosition, Quaternion.identity);
        
        if (!enemyInstance.TryGetComponent<EnemyInitializer>(out var initializer))
        {
            Debug.LogError($"Enemy prefab {enemyType.name} does not have an EnemyInitializer component.");
            return;
        }
        else
        {
            initializer.Initialize(enemyType);
        }

            enemyInstance.GetComponent<EnemyInitializer>().Initialize(enemyType);
        if (enemyInstance == null )
        {
            Debug.LogError($"Failed to spawn enemy of type {enemyType.name}");
            return;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return playerTransform.position + new Vector3(UnityEngine.Random.Range(-30, 30), 0, UnityEngine.Random.Range(-30, 30));
    }
}
