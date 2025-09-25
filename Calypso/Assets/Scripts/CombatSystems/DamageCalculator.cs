using UnityEngine;

public static class DamageCalculator
{
    public static DamageInfo GetDamageInfo(WeaponDefinitionSO weaponData)
    {
        return new DamageInfo
        {
            damage = weaponData.baseDamage * PlayerManager.Instance.GetStrength(),
            knockbackStrength = weaponData.baseKnockback * PlayerManager.Instance.GetStrength(),
            stunDuration = weaponData.baseStun * PlayerManager.Instance.GetDexterity(),
            poisonDuration = weaponData.basePoison * PlayerManager.Instance.GetDuration(),
            slowDuration = weaponData.baseSlowdown * PlayerManager.Instance.GetDuration(),
        };
    }

    public static DamageInfo GetDamageInfo(EnemyDefinitionSO enemyData)
    {
        return enemyData.MakeDamageInfo();
    }
}

