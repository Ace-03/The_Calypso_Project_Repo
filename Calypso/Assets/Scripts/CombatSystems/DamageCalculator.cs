using UnityEngine;

public static class DamageCalculator
{
    public static DamageInfo GetDamageInfo(WeaponDefinitionSO weaponData)
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

public struct DamageInfo
{
    public float damage;
    public float knockbackStrength;
    public float stunDuration;
    public float poisonDuration;
    public float slowDuration;
}

