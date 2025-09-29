using System;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    private EnemyDefinitionSO enemyData;
    private EnemyHealth healthSystem;
    private PooledObject pooledObject;
    private SpriteRenderer sr;
    private IEnemyMovement ai;

    private void Awake()
    {
        if (!TryGetComponent<PooledObject>(out pooledObject))
            pooledObject = gameObject.AddComponent<PooledObject>();
        
        if (!TryGetComponent<EnemyHealth>(out healthSystem))
            healthSystem = gameObject.AddComponent<EnemyHealth>();
        
        if (!TryGetComponent<IEnemyMovement>(out ai))
            ai = gameObject.AddComponent<AI_NAV>();

        if (!GetComponentInChildren<SpriteRenderer>())
            sr = Instantiate(new GameObject("Sprite"), transform).AddComponent<SpriteRenderer>();
        else
            sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(EnemyDefinitionSO data)
    {
        enemyData = data;

        InitializeHealth(data);
        InitializeMovement(data);
        InitializeSprite(data);
        InitializeWeapon(data.weapon);

        // Initialize other components like health, speed, etc. based on enemyData
    }

    private void InitializeHealth(EnemyDefinitionSO data)
    {
        healthSystem.Initialize(new HealthData { maxHP = data.maxHealth });
    }

    private void InitializeMovement(EnemyDefinitionSO data)
    {
        ai.SetSpeed(data.movementSpeed);
    }

    private void InitializeSprite(EnemyDefinitionSO data)
    {
        sr.sprite = data.sprite;
        sr.color = Color.white;
    }

    private void InitializeWeapon(WeaponDefinitionSO weaponData)
    {
        if (weaponData == null)
        {
            Debug.LogError("Weapon data is null in EnemyInitializer. Enemy will be unarmed.");
            return;
        }

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
        PickupSpawner.RollForItemDrop(enemyData, transform.position);
        pooledObject.DeactivateAndReturn();
    }
}
