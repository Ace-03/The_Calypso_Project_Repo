using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponDefinitionSO weaponData;
    [SerializeField]
    public Transform weaponPivot;
    [SerializeField]
    private bool autoInitialize = false;

    public readonly Dictionary<string, float> currentStats = new Dictionary<string, float>();

    private GameObject weaponInstance;
    private IWeaponBehavior weaponBehavior;
    private float nextAttackTime;

    private void Start()
    {
        if (autoInitialize)
            Initialize();
    }

    public void Initialize()
    {
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

        if (weaponPivot == null)
        {
            if (transform.Find("WeaponPivot") != null)
            {
                weaponPivot = transform.Find("WeaponPivot");
            }
            else
            {
                GameObject pivotObject = Instantiate(new GameObject(), transform);

                weaponPivot = pivotObject.transform;
                pivotObject.name = "WeaponPivot";
                pivotObject.tag = "Enemy";
                pivotObject.layer = 7;
            }
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

    private void CheckForStat(string stat)
    {
        if (!currentStats.ContainsKey(stat))
        {
            RecalculateStats();

            if (!currentStats.ContainsKey(stat))
            {
                Debug.LogError($"Stat {stat} not found in currentStats after recalculation. Setting to 0.");
                currentStats[stat] = 0;
            }
        }
    }

    public float GetCooldown()
    {
        CheckForStat("Cooldown");
        return currentStats["Cooldown"];
    }
    public int GetAmount()
    {
        CheckForStat("Amount");
        return (int)currentStats["Amount"];
    }
    public float GetDuration()
    {
        CheckForStat("Duration");
        return currentStats["Duration"];
    }
    public float GetSpeed()
    {
        CheckForStat("Speed");
        return currentStats["Speed"];
    }
    public float GetAOETick()
    {
        CheckForStat("AOETick");
        return currentStats["AOETick"];
    }
    public float GetArea()
    {
        CheckForStat("Area");
        return currentStats["Area"];
    }

    public float GetAccuracy()
    {
        CheckForStat("accuracy");
        return currentStats["accuracy"];
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
        if (weaponInstance == null)
        {
            Debug.LogWarning("No weapon on weapon controller: " + name);
            return;
        }

        weaponInstance.SetActive(false);
    }
}
