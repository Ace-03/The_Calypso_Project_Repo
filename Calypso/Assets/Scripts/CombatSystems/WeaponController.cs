using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponDefinitionSO weaponData;
    [SerializeField] private bool autoInitialize = false;

    public readonly Dictionary<string, float> currentStats = new Dictionary<string, float>();

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

        if (weaponPivot == null)
            MakeWeaponPivot();
        else
            RenamePivot();

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

        currentStats["Cooldown"] = weaponData.baseCooldown * stats.GetFinalValue(StatType.Cooldown);
        currentStats["Amount"] = weaponData.baseAmount + stats.GetFinalValue(StatType.Amount);
        currentStats["Duration"] = weaponData.baseDuration * stats.GetFinalValue(StatType.Duration);
        currentStats["accuracy"] = weaponData.baseAccuracy + stats.GetFinalValue(StatType.Dexterity);
        currentStats["Speed"] = weaponData.baseProjectileSpeed * stats.GetFinalValue(StatType.Dexterity);
        currentStats["AOETick"] = weaponData.aoeTickRate * stats.GetFinalValue(StatType.Cooldown);
        currentStats["Area"] = weaponData.aoeAreaSize * stats.GetFinalValue(StatType.Size);
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
        if (tag == "Player")
        {
            GameObject pivotObject = Instantiate(new GameObject(), transform);
            pivotObject.name = $"{weaponData.weaponName} Pivot";
            pivotObject.tag = "Player";
            pivotObject.layer = 8;

            weaponPivot = pivotObject.transform;
            Debug.Log("Created Player Weapon Pivot");
        }
        else if (tag == "Enemy")
        {
            GameObject pivotObject = Instantiate(new GameObject(), transform);
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
        if (weaponInstance == null) return;

        weaponInstance.SetActive(false);
    }

    private void OnEnable()
    {
        if (weaponInstance == null) return;
        weaponInstance.SetActive(true);
    }
}
