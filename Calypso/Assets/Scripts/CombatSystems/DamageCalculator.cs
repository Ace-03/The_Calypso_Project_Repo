using UnityEngine;

public static class DamageCalculator
{
    public static DamageInfo GetDamageFromPlayer(WeaponDefinitionSO weaponData)
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

    public static DamageInfo GetDamageToPlayer(EnemyDefinitionSO enemyData)
    {
        DamageInfo damageInfo = enemyData.MakeDamageInfo();
        damageInfo.damage *= 1 + (PlayerManager.Instance.GetArmor() * -0.01f);
        return damageInfo;
    }
}

