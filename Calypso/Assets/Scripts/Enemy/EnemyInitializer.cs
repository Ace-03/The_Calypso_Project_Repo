using System;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    private EnemyDefinitionSO enemyData;
    private EnemyHealth healthSystem;
    private VisualEffectsHandler VfxHandler;
    private PooledObject pooledObject;
    private SpriteRenderer sr;
    private IEnemyMovement ai;

    private Color deathParticleColor;

    private void Awake()
    {
        if (!TryGetComponent<PooledObject>(out pooledObject))
            pooledObject = gameObject.AddComponent<PooledObject>();
        
        if (!TryGetComponent<EnemyHealth>(out healthSystem))
            healthSystem = gameObject.AddComponent<EnemyHealth>();

        if (!TryGetComponent<VisualEffectsHandler>(out var vfx))
            VfxHandler = gameObject.AddComponent<VisualEffectsHandler>();

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
        InitializeVFX();
        InitializeWeapon(data.weapon);
    }

    private void InitializeHealth(EnemyDefinitionSO data)
    {
        healthSystem.Initialize(new HealthData { maxHP = data.maxHealth });
    }

    private void InitializeVFX()
    {
        VfxHandler.Initialize(sr, healthSystem.maxHP);
    }

    private void InitializeMovement(EnemyDefinitionSO data)
    {
        ai.SetSpeed(data.movementSpeed);
    }

    private void InitializeSprite(EnemyDefinitionSO data)
    {
        sr.sprite = data.sprite;
        sr.color = data.spriteColor;
        transform.localScale *= data.sizeModifier;
        deathParticleColor = SpriteAverageColor.GetAverageColor(data.sprite) * data.spriteColor;
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
        weaponController.Initialize();

        if (weaponController.GetWeaponBehavior().IsAimable())
        {
            if (!TryGetComponent<EnemyWeaponAim>(out var aim))
                aim = gameObject.AddComponent<EnemyWeaponAim>();

            aim.Initialize(weaponController, enemyData.aimSpeed);
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

        RemoveEnemy();

        if (enemyData.deathEffect != null)
        {
            ParticleSystem ps = Instantiate(enemyData.deathEffect, transform.position, Quaternion.identity)
                .GetComponentInChildren<ParticleSystem>();

            GeneralModifier.SetColor(ps, deathParticleColor);
        }
    }

    void RemoveEnemy()
    {
        pooledObject.DeactivateAndReturn();
    }
}
