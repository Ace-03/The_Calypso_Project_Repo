using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private OnEnemyDeathEventSO enemyDeathEvent;
    [SerializeField] private OnDayStateChangeEventSO dayStateChangeEvent;
    [SerializeField] private OnDeathEventSO deathEvent;

    [Header("Parameters")]
    [Tooltip("Distance to sample NavMesh for valid spawn positions.")]
    [SerializeField] private float navMeshSampleDistance = 10.0f;
    [SerializeField] private bool spawnOnStart = false;
    [SerializeField] private int poolSize = 80;
    [SerializeField] private List<EnemyInitializer> activeEnemies = new List<EnemyInitializer>();
    public List<WaveSequenceDefinitionSO> waveComposite;
    private Transform playerTransform;

    private WaveSequenceDefinitionSO currentSequence;
    private int currentWaveIndex = -1;
    private float waveTimer = 0f;

    private bool spawningActive = false;

    private void Start()
    {
        playerTransform = ContextRegister.Instance.GetContext().playerTransform;
        
        if (spawnOnStart && waveComposite.Count > 0)
        {
            Invoke(nameof(StartSpawning), 2f);
        }
    }

    private void OnEnable()
    {
        enemyDeathEvent.RegisterListener(RemoveActiveEnemy);
        dayStateChangeEvent.RegisterListener(OnDayStateChanged);
        deathEvent.RegisterListener(StopSpawning);
    }

    private void OnDisable()
    {
        enemyDeathEvent.UnregisterListener(RemoveActiveEnemy);
        dayStateChangeEvent.UnregisterListener(OnDayStateChanged);
        deathEvent.UnregisterListener(StopSpawning);
    }

    private void StopSpawning(DeathPayload payload) => ToggleSpawning(false);

    private void StartSpawning()
    {
        SetCurrentWave(0);
        ToggleSpawning(true);
    }

    public void ToggleSpawning(bool toggle)
    {
        if (toggle == false)
        {
            StopAllCoroutines();
            Debug.Log("Spawning set to false");

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

    public void RemoveEnemies()
    {
        foreach(EnemyInitializer enem in activeEnemies)
        {
            enem.RemoveEnemy();
        }

        activeEnemies.Clear();
        Debug.Log("Cleared active enemies from scene");
    }

    private void RemoveActiveEnemy(DeathPayload payload)
    {
        EnemyInitializer enemyInit = payload.entity.GetComponent<EnemyInitializer>();

        if (enemyInit != null && activeEnemies.Contains(enemyInit))
        {
            activeEnemies.Remove(enemyInit);
            Debug.Log($"Removed enemy {payload.entity.name} from active enemy List");
        }
    }

    private void OnDayStateChanged(DayStateChangePayload payload)
    {
        if (payload.isDayTime)
        {
            ToggleSpawning(false);
            ResetSpawner();
        }
        else
        {
            SetCurrentWave(payload.dayCount - 1);
            ToggleSpawning(true);
        }
    }

    public void SetCurrentWave(WaveSequenceDefinitionSO sequence)
    {
        currentSequence = sequence;
    }

    public void SetCurrentWave(int index)
    {
        if (index >= waveComposite.Count)
        {
            Debug.LogError($"Wave index of {index} is larger than wave composite count of {waveComposite.Count}");
            return;
        }

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
            PoolManager.Instance.CreatePool(enemyData.name, enemyData.enemyPrefab, poolSize);
        }
    }

    private void Update()
    {
        if (!spawningActive) return;

        if (waveTimer <= 0)
        {
            ChangeWave();
        }
        waveTimer -= Time.deltaTime;
    }

    private void ChangeWave()
    {
        StopAllCoroutines();
        currentWaveIndex++;

        if (currentWaveIndex < currentSequence.waveDefinitions.Count)
        {
            Debug.Log("Found Wave index");
            waveTimer = currentSequence.waveDefinitions[currentWaveIndex].waveDuration;

            foreach (EnemySpawnInfo spawnInfo in currentSequence.waveDefinitions[currentWaveIndex].enemiesInWave)
            {
                StartCoroutine(SpawnEnemies(spawnInfo));
            }
        }
        else
        {
            ToggleSpawning(false);
        }
    }

    private IEnumerator SpawnEnemies(EnemySpawnInfo spawnInfo)
    {
        Debug.Log($"Coroutine is active for {spawnInfo.enemyDefinition.enemyName}");
        while (true)
        {
            float t = (spawnInfo.spawnRate - 1f) / (100f - 1f);
            float spawnInterval = Mathf.Lerp(5f, 0.001f, t);

            Debug.Log($"Spawn interval for {spawnInfo.enemyDefinition.enemyName} is {spawnInterval}");

            float totalEnemies = FindObjectsByType<EnemyInitializer>(FindObjectsSortMode.None).Length;

            foreach (var enemy in FindObjectsByType<EnemyInitializer>(FindObjectsSortMode.None))
            {
                if (enemy.GetEnemyData().enemyName != spawnInfo.enemyDefinition.enemyName)
                {
                    totalEnemies--;
                }
            }

            if (totalEnemies < spawnInfo.maxActiveEnemies)
            {
                //Debug.Log("Spawning enemy");
                GameObject newEnemy = SpawnEnemy(spawnInfo.enemyDefinition);

                if (newEnemy != null)
                {
                    activeEnemies.Add(newEnemy.GetComponent<EnemyInitializer>());
                }
            }
            else
            {
                //Debug.Log($"Max active enemies reached for {enemySpawnInfo.enemyDefinition.name}");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private GameObject SpawnEnemy(EnemyDefinitionSO enemyType)
    {
        Nullable<Vector3> spawnPosition = GetRandomSpawnPosition();
        GameObject enemyInstance = null;

        if (spawnPosition.HasValue)
        {
            enemyInstance = PoolManager.Instance.GetFromPool(enemyType.name, spawnPosition.Value, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Failed To Spawn Enemy of Type {enemyType.name}. could not find position");
            return null; 
        }

        if (!enemyInstance.TryGetComponent<EnemyInitializer>(out var initializer))
        {
            Debug.LogError($"Enemy prefab {enemyType.name} does not have an EnemyInitializer component.");
            return null;
        }
        else
        {
            initializer.Initialize(enemyType);
        }

        if (enemyInstance == null )
        {
            Debug.LogError($"Failed to spawn enemy of type {enemyType.name}, enemy instance null");
            return null;
        }

        return enemyInstance;
    }

    private Nullable<Vector3> GetRandomSpawnPosition()
    {
        Vector3 randomPosition = playerTransform.position + new Vector3(UnityEngine.Random.Range(-30, 30), 0, UnityEngine.Random.Range(-30, 30));

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return null;
        }
    }
}
