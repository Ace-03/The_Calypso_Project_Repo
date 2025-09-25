using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField]
    private bool useDefaultComponents = true;

    private EnemyDefinitionSO enemyData;
    private EnemyHealth healthSystem;
    private PooledObject pooledObject;
    private IEnemyMovement ai;
    private void Awake()
    {
        // If we are using components from the SO then don't add defaults.
        if (!useDefaultComponents) return;

        if (!TryGetComponent<PooledObject>(out pooledObject))
            pooledObject = gameObject.AddComponent<PooledObject>();
        if (!TryGetComponent<EnemyHealth>(out healthSystem))
            healthSystem = gameObject.AddComponent<EnemyHealth>();
        if (!TryGetComponent<IEnemyMovement>(out ai))
            ai = gameObject.AddComponent<AI_NAV>();
    }

    public void Initialize(EnemyDefinitionSO data)
    {
        enemyData = data;

        healthSystem.Initialize(new HealthData { maxHP = data.maxHealth });
        ai.SetSpeed(data.movementSpeed);
        InitializeWeapon(data.weapon);

        // Initialize other components like health, speed, etc. based on enemyData
    }

    private void InitializeWeapon(WeaponDefinitionSO weaponData)
    {
        if (!TryGetComponent<WeaponController>(out var weaponController))
            weaponController = gameObject.AddComponent<WeaponController>();

        weaponController.SetWeaponData(weaponData);


        if (weaponController.GetWeaponBehavior().IsAimable())
        {
            if (!TryGetComponent<EnemyWeaponAim>(out var aim))
                aim = gameObject.AddComponent<EnemyWeaponAim>();

            aim.Initialize(weaponController.GetWeaponInstance(), enemyData.aimSpeed);
        }
    }

    public EnemyDefinitionSO GetEnemyData()
    {
        if (enemyData == null)
        {
            throw new InvalidOperationException("Enemy data has not been initialized.");
        }

        return enemyData;
    }

    internal void OnDeath()
    {
        pooledObject.DeactivateAndReturn();
    }
}
