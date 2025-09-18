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
        waveTimer = waveSequence[0].waveDuration;
        StartCoroutine(SpawnEnemies());
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
                    SpawnEnemy();
                }

                yield return new WaitForSeconds(spawnInterval);
            }   
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject enemy = Instantiate(waveSequence[currentWaveIndex].enemiesInWave[0].enemyDefinition.enemyPrefab, spawnPosition, Quaternion.identity);

        // spawn from object pool
        //GameObject enemy = ObjectPool.Instance.GetPooledObject("Enemy");
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return playerTransform.position + new Vector3(UnityEngine.Random.Range(-20, 20), 0, UnityEngine.Random.Range(-10, 10));
    }
}
