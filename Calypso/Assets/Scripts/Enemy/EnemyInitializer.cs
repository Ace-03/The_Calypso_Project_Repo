using System;
using System.Collections;
using UnityEngine;

public class EnemyInitializer : MonoBehaviour
{
    [SerializeField]
    private OnEnemyDeathEventSO deathEvent;

    private Animator animator;
    private EnemyDefinitionSO enemyData;
    private EnemyHealth healthSystem;
    private EnemySpriteBob bob;
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

        if (!TryGetComponent<EnemySpriteBob>(out bob))
            bob = gameObject.AddComponent<EnemySpriteBob>();

        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("couldn't find animator on: " + name);
        }

        if (!GetComponentInChildren<SpriteRenderer>())
        {
            sr = Instantiate(new GameObject("Sprite"), transform).AddComponent<SpriteRenderer>();
            sr.gameObject.name = "Sprite object";
        }
        else
            sr = GetComponentInChildren<SpriteRenderer>();
    }

    public void Initialize(EnemyDefinitionSO data)
    {
        enemyData = data;

        InitializeHealth(data);
        InitializeMovement(data);
        InitializeSprite(data);
        InitializeVFX(data);
        InitializeWeapon(data.weapon);
        InitializeVFX(data);

        StartCoroutine(ResetAnimator());
    }

    private void InitializeHealth(EnemyDefinitionSO data)
    {
        healthSystem.Initialize(new HealthData { maxHP = data.maxHealth });
    }

    private void InitializeVFX(EnemyDefinitionSO data)
    {
        bob.Initialize(data, sr);
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
        sr.transform.localScale *= data.sizeModifier;
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
        weaponController.SetDamageSource(new DamageSource(weaponData, gameObject, enemyData));
        weaponController.Initialize();

        if (weaponController.GetWeaponBehavior().IsAimable())
        {
            if (!TryGetComponent<EnemyWeaponAim>(out var aim))
                aim = gameObject.AddComponent<EnemyWeaponAim>();

            aim.Initialize(weaponController, enemyData.aimSpeed);
        }
    }

    private IEnumerator ResetAnimator()
    {
        animator.enabled = true;

        yield return new WaitForSeconds(0.4f);

        animator.enabled = false;
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

        DeathPayload payload = new DeathPayload()
        {
            entity = gameObject,
        };

        deathEvent.Raise(payload);

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
