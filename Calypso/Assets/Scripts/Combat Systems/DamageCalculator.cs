using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    public static DamageCalculator Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public DamageInfo GetDamageInfo(WeaponDefinitionSO weaponData)
    {
        return new DamageInfo
        {
            damage = weaponData.baseDamage * PlayerStats.Instance.GetStrength(),
            knockbackStrength = weaponData.baseKnockback * PlayerStats.Instance.GetStrength(),
            stunDuration = weaponData.baseStun * PlayerStats.Instance.GetDexterity(),
            poisonDuration = weaponData.basePoison * PlayerStats.Instance.GetDuration(),
            slowDuration = weaponData.baseSlowdown * PlayerStats.Instance.GetDuration(),
        };

    }
}

public class DamageInfo
{
    public float damage;
    public float knockbackStrength;
    public float stunDuration;
    public float poisonDuration;
    public float slowDuration;
}

