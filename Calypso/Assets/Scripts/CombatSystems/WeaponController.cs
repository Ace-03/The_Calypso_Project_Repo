using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponDefinitionSO weaponData;
    [SerializeField] private bool autoInitialize = false;

    public WeaponStats currentStats;

    public TEAM team;
    public Transform weaponPivot;
    private GameObject weaponInstance;
    private IWeaponBehavior weaponBehavior;
    private float nextAttackTime;

    private StatSystem stats;

    private void Awake()
    {
        if (autoInitialize)
            Initialize();
    }
    public void Initialize()
    {
        stats = ContextRegister.Instance.GetContext().statSystem;
     
        if (weaponData == null)
        {
            Debug.LogError("Weapon data is not assigned in WeaponController. Weapon is Likely Missing.");
            return;
        }
        InitializeData();
        RecalculateStats();
        weaponBehavior?.ApplyWeaponStats(this);
        nextAttackTime = Time.time + GetCooldown();

    }

    private void Update()
    {
        if (weaponData == null)
        {
            Debug.LogError("Weapon data is not assigned in WeaponController. Weapon is Likely Missing.");
            return;
        }
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + GetCooldown();
        }
    }

    private void InitializeData()
    {
        if (weaponInstance != null)
            Destroy(weaponInstance);

        if (weaponData == null)
        {
            Debug.LogError("Weapon data is not assigned in WeaponController. Weapon is Likely Missing.");
            return;
        }

        if (CompareTag("Player"))
            team = TEAM.Player;
        else if (CompareTag("Enemy"))
            team = TEAM.Enemy;

        if (weaponData.pierce)
            team = TEAM.PlayerPierce;

        if (weaponPivot == null)
            MakeWeaponPivot();
        else
            RenamePivot();

        weaponInstance = Instantiate(weaponData.weaponBehaviorPrefab, weaponPivot);
        weaponBehavior = weaponInstance.GetComponent<IWeaponBehavior>();
    }

    public void RecalculateStats()
    {
        if (weaponData == null)
        {
            Debug.LogError("Weapon data is not assigned in WeaponController. Setting stats to zero");
            currentStats = new WeaponStats();
            return;
        }

        currentStats = weaponData.baseStats;

        if (CompareTag("Player"))
        {
            currentStats.Cooldown = weaponData.baseStats.Cooldown * stats.GetFinalValue(StatType.Cooldown);
            currentStats.Amount = (int)(weaponData.baseStats.Amount + stats.GetFinalValue(StatType.Amount));
            currentStats.Duration = weaponData.baseStats.Duration * stats.GetFinalValue(StatType.Duration);
            currentStats.accuracy = weaponData.baseStats.accuracy + stats.GetFinalValue(StatType.Dexterity);
            currentStats.Speed = weaponData.baseStats.Speed * stats.GetFinalValue(StatType.Dexterity);
            currentStats.AOETick = weaponData.baseStats.AOETick * stats.GetFinalValue(StatType.Cooldown);
            currentStats.Area = weaponData.baseStats.Area * stats.GetFinalValue(StatType.Size);
        }
    }

    public void Attack()
    {
        weaponBehavior?.Attack(this);
    }

    public WeaponDefinitionSO GetWeaponData()
    {
        return weaponData;
    }

    public void SetWeaponData(WeaponDefinitionSO data)
    {
        weaponData = data;
        Initialize();
    }

    public void DestroyWeaponInstance()
    {
        if (weaponInstance != null) { Destroy(weaponInstance); }
        if (weaponPivot != null) { Destroy(weaponPivot); }
    }

    private void MakeWeaponPivot()
    {
        if (CompareTag("Player"))
        {
            GameObject pivotObject = Instantiate(new GameObject(), transform);
            pivotObject.name = $"{weaponData.weaponName} Pivot";
            pivotObject.tag = "Player";
            pivotObject.layer = 8;

            weaponPivot = pivotObject.transform;
            Debug.Log("Created Player Weapon Pivot");
        }
        else if (CompareTag("Enemy"))
        {
            Debug.Log("pivot container: " + transform.Find("Pivot Container").name);
            GameObject pivotObject = Instantiate(new GameObject(), transform.Find("Pivot Container"));
            pivotObject.name = $"{weaponData.weaponName} Pivot";
            pivotObject.tag = "Enemy";
            pivotObject.layer = 7;

            weaponPivot = pivotObject.transform;
        }
    }

    private void RenamePivot()
    {
        weaponPivot.name = $"{weaponData.weaponName} Pivot";
    }

    #region Getters
    public float GetCooldown()
    {
        return currentStats.Cooldown;
    }
    public int GetAmount()
    {
        return currentStats.Amount;
    }
    public float GetDuration()
    {
        return currentStats.Duration;
    }
    public float GetSpeed()
    {
        return currentStats.Speed;
    }
    public float GetAOETick()
    {
        return currentStats.AOETick;
    }
    public float GetArea()
    {
        return currentStats.Area;
    }

    public float GetAccuracy()
    {
        return currentStats.accuracy;
    }

    public Material GetSprite()
    {
        if (weaponData != null)
            return weaponData.bulletSprite;
        else
            return null;
    }
    public GameObject GetWeaponInstance()
    {
        return weaponInstance;
    }

    public IWeaponBehavior GetWeaponBehavior()
    {
        return weaponBehavior;
    }

    #endregion

    private void OnDisable()
    {
        if (weaponInstance == null) return;

        weaponInstance.SetActive(false);
    }

    private void OnEnable()
    {
        if (weaponInstance == null) return;
        weaponInstance.SetActive(true);
    }
}
