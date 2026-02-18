using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private List<Pool> pools;
    private Dictionary<string, Pool> poolDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        poolDictionary = new Dictionary<string, Pool>();
        foreach (var pool in pools)
        {
            pool.Initialize(transform);
            poolDictionary.Add(pool.name, pool);
        }
    }

    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
        private Queue<GameObject> objectQueue;

        public void Initialize(Transform parent)
        {
            objectQueue = new Queue<GameObject>();
            for (int i = 0; i < size; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                obj.SetActive(false);
                obj.GetComponent<PooledObject>().poolName = name;
                objectQueue.Enqueue(obj);
            }
        }

        public GameObject GetObject(Vector3 position, Quaternion rotation)
        {
            if (objectQueue.Count == 0)
            {
                // Optionally grow the pool if it's empty
                Debug.LogWarning($"Pool '{name}' is exhausted. Growing the pool.");
                GameObject obj = GameObject.Instantiate(prefab);
                obj.GetComponent<PooledObject>().poolName = name;
                return obj;
            }

            GameObject objToSpawn = objectQueue.Dequeue();
            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;
            objToSpawn.SetActive(true);
            return objToSpawn;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogError($"Pool with name '{poolName}' does not exist.");
            return null;
        }
        return poolDictionary[poolName].GetObject(position, rotation);
    }

    public void ReturnToPool(string poolName, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning($"Pool with name '{poolName}' does not exist.");
            Destroy(obj);
            return;
        }
        poolDictionary[poolName].ReturnObject(obj);
    }

    internal void CreatePool(string poolName, GameObject enemyPrefab, int size)
    {
        if (poolDictionary.ContainsKey(poolName))
        {
            Debug.LogWarning($"Pool '{poolName}' already exists.");
            return;
        }

        Pool newPool = new Pool
        {
            name = poolName,
            prefab = enemyPrefab,
            size = size
        };

        newPool.Initialize(transform);
        poolDictionary.Add(poolName, newPool);

        Debug.Log($"Created new pool '{poolName}' with size {size}.");
    }
}