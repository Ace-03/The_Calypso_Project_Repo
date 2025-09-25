using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponDefinitionSO weaponData;

    public readonly Dictionary<string, float> currentStats = new Dictionary<string, float>();

    private GameObject weaponInstance;
    private IWeaponBehavior weaponBehavior;
    private float nextAttackTime;

    private void Start()
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
        weaponInstance = Instantiate(weaponData.weaponBehaviorPrefab, transform);
        weaponBehavior = weaponInstance.GetComponent<IWeaponBehavior>();
    }

    public void RecalculateStats()
    {
        currentStats.Clear();

        currentStats["Cooldown"] = weaponData.baseCooldown * PlayerStats.Instance.GetCooldown();
        currentStats["Amount"] = weaponData.baseAmount + PlayerStats.Instance.GetAmount();
        currentStats["Duration"] = weaponData.baseDuration * PlayerStats.Instance.GetDuration();
        currentStats["accuracy"] = weaponData.baseAccuracy + PlayerStats.Instance.GetDexterity();
        currentStats["Speed"] = weaponData.baseProjectileSpeed * PlayerStats.Instance.GetDexterity();
        currentStats["AOETick"] = weaponData.aoeTickRate * PlayerStats.Instance.GetCooldown();
        currentStats["Area"] = weaponData.aoeAreaSize * PlayerStats.Instance.GetArea();
    }

    public void Attack()
    {
        weaponBehavior?.Attack(this);
    }

    public WeaponDefinitionSO GetWeaponData()
    {
        return weaponData;
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

    #endregion
}
