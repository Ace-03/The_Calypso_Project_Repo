using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    EnemyDefinitionSO enemyData;

    HealthSystem healthSystem;
    PooledObject pooledObject;

    private void Awake()
    {
        if (!TryGetComponent<PooledObject>(out pooledObject))
        {
            pooledObject = gameObject.AddComponent<PooledObject>();
        }

        healthSystem = GetComponent<HealthSystem>();

        Initialize(enemyData);
    }

    public void Initialize(EnemyDefinitionSO data)
    {
        healthSystem.maxHP = data.maxHealth;
        // Initialize other components like health, speed, etc. based on enemyData
    }


}
