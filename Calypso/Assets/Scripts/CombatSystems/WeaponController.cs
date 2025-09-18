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
        nextAttackTime = Time.time + currentStats["Cooldown"];

        Debug.Log(nextAttackTime);
        Debug.Log(Time.time);
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + currentStats["Cooldown"];
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
}
