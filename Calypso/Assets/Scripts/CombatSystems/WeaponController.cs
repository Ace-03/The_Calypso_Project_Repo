using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponDefinitionSO weaponData;
    [SerializeField]
    public Transform weaponPivot;

    public readonly Dictionary<string, float> currentStats = new Dictionary<string, float>();

    private GameObject weaponInstance;
    private IWeaponBehavior weaponBehavior;
    private float nextAttackTime;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        InitializeData();
        RecalculateStats();
        weaponBehavior?.ApplyWeaponStats(this);
        nextAttackTime = Time.time + GetCooldown();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + GetCooldown();
        }
    }

    public void InitializeData()
    {
        if (weaponInstance != null)
            Destroy(weaponInstance);
        
        if (weaponData == null)
        {
            Debug.LogError("Weapon data is not assigned in WeaponController. Weapon is Likely Missing.");
            return;
        }

        if (weaponPivot == null)
        {
            GameObject pivotObject = Instantiate(new GameObject(), transform);

            weaponPivot = pivotObject.transform;
            pivotObject.name = "Weapon Pivot";
            pivotObject.tag = "Enemy";
            pivotObject.layer = 7;
        }

        weaponInstance = Instantiate(weaponData.weaponBehaviorPrefab, weaponPivot);
        weaponBehavior = weaponInstance.GetComponent<IWeaponBehavior>();
    }

    public void RecalculateStats()
    {
        currentStats.Clear();

        if (weaponData == null)
        {
            Debug.LogError("Weapon data is not assigned in WeaponController. Setting stats to zero");
            currentStats["Cooldown"] = 0;
            currentStats["Amount"] = 0;
            currentStats["Duration"] = 0;
            currentStats["accuracy"] = 0;
            currentStats["Speed"] = 0;
            currentStats["AOETick"] = 0;
            currentStats["Area"] = 0;
            return;
        }


        if (!CompareTag("Player"))
        {
            currentStats["Cooldown"] = weaponData.baseCooldown;
            currentStats["Amount"] = weaponData.baseAmount;
            currentStats["Duration"] = weaponData.baseDuration;
            currentStats["accuracy"] = weaponData.baseAccuracy;
            currentStats["Speed"] = weaponData.baseProjectileSpeed;
            currentStats["AOETick"] = weaponData.aoeTickRate;
            currentStats["Area"] = weaponData.aoeAreaSize;
            return;
        }

        currentStats["Cooldown"] = weaponData.baseCooldown * PlayerManager.Instance.GetCooldown();
        currentStats["Amount"] = weaponData.baseAmount + PlayerManager.Instance.GetAmount();
        currentStats["Duration"] = weaponData.baseDuration * PlayerManager.Instance.GetDuration();
        currentStats["accuracy"] = weaponData.baseAccuracy + PlayerManager.Instance.GetDexterity();
        currentStats["Speed"] = weaponData.baseProjectileSpeed * PlayerManager.Instance.GetDexterity();
        currentStats["AOETick"] = weaponData.aoeTickRate * PlayerManager.Instance.GetCooldown();
        currentStats["Area"] = weaponData.aoeAreaSize * PlayerManager.Instance.GetArea();
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

    #region Getters

    public float GetCooldown()
    {
        return currentStats["Cooldown"];
    }
    public int GetAmount()
    {
        return (int)currentStats["Amount"];
    }
    public float GetDuration()
    {
        return currentStats["Duration"];
    }
    public float GetSpeed()
    {
        return currentStats["Speed"];
    }
    public float GetAOETick()
    {
        return currentStats["AOETick"];
    }
    public float GetArea()
    {
        return currentStats["Area"];
    }

    public float GetAccuracy()
    {
        return currentStats["accuracy"];
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
}
