using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    EnemyDefinitionSO enemyData;

    private HealthSystem healthSystem;
    private PooledObject pooledObject;

    private void Awake()
    {
        if (!TryGetComponent<PooledObject>(out pooledObject))
        {
            pooledObject = gameObject.AddComponent<PooledObject>();
        }

        healthSystem = GetComponent<HealthSystem>();

        Initialize(enemyData);
    }

    private void Initialize(EnemyDefinitionSO data)
    {
        healthSystem.maxHP = data.maxHealth;
        healthSystem.hp = data.maxHealth;
        // Initialize other components like health, speed, etc. based on enemyData
    }

    public EnemyDefinitionSO GetEnemyData()
    {
        return enemyData;
    }

    internal void OnDeath()
    {
        pooledObject.DeactivateAndReturn();
    }
}
