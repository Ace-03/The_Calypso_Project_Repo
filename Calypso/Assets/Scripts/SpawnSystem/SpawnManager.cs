using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<WaveSequenceDefinitionSO> waveComposite;
    public Transform playerTransform;

    private WaveSequenceDefinitionSO currentSequence;
    private int currentWaveIndex = -1;
    private float waveTimer = 0f;

    private bool spawningActive = false;
    public void ToggleSpawning(bool toggle)
    {
        if (toggle == false)
        {
            StopAllCoroutines();
        }
        else
        {
            Debug.Log("Spawning set to true");
        }
            spawningActive = toggle;
    }

    public void ResetSpawner()
    {
        waveTimer = 0f;
        currentWaveIndex = -1;
    }

    public void SetCurrentWave(WaveSequenceDefinitionSO sequence)
    {
        currentSequence = sequence;
    }

    public void SetCurrentWave(int index)
    {
        if (index >= waveComposite.Count)
            return;

        currentSequence = waveComposite[index];
        InitializeEnemyPools();
    }

    private void InitializeEnemyPools()
    {
        HashSet<EnemyDefinitionSO> uniqueEnemies = new HashSet<EnemyDefinitionSO>();
        
        foreach (var wave in currentSequence.waveDefinitions)
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
        if (!spawningActive)
        {
            return;
        }

        if (waveTimer <= 0)
        {
            currentWaveIndex++;

            if (currentWaveIndex < currentSequence.waveDefinitions.Count)
            {
                Debug.Log("Found Wave index");
                waveTimer = currentSequence.waveDefinitions[currentWaveIndex].waveDuration;
                StartCoroutine(SpawnEnemies());
            }
            else
            {
                StopAllCoroutines();
            }
        }
        waveTimer -= Time.deltaTime;
    }

    private IEnumerator SpawnEnemies()
    {
        Debug.Log("Coroutine is active");
        while (true)
        {
            WaveDefinitionSO currentWave = currentSequence.waveDefinitions[currentWaveIndex];

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
                    Debug.Log("Spawning enemy");
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
