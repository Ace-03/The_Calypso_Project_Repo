using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponDefinitionSO weaponData;

    private IWeaponBehavior weaponBehavior;
    private readonly Dictionary<string, float> currentStats = new Dictionary<string, float>();
    private float nextAttackTime;

    private void Start()
    {
        InitializeData();
        RecalculateStats();
        nextAttackTime = Time.time + currentStats["Cooldown"];
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
        weaponBehavior = weaponData.weaponBehaviorPrefab.GetComponent<IWeaponBehavior>();
    }

    public void RecalculateStats()
    {
        currentStats.Clear();

        currentStats["Cooldown"] = weaponData.baseCooldown * PlayerStats.Instance.GetCooldown();
        currentStats["Ammount"] = weaponData.baseAmmount + PlayerStats.Instance.GetAmmount();
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
