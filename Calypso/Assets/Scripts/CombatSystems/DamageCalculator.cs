using UnityEngine;

public static class DamageCalculator
{
    public static DamageInfo GetDamageFromPlayer(WeaponDefinitionSO weaponData)
    {
        StatSystem stats = ContextRegister.Instance.GetContext().statSystem;

        return new DamageInfo
        {
            damage = weaponData.baseStats.Damage * stats.GetFinalValue(StatType.Strength),
            knockbackStrength = weaponData.baseKnockback * stats.GetFinalValue(StatType.Strength),
            stunDuration = weaponData.baseStun * stats.GetFinalValue(StatType.Dexterity),
            poisonDuration = weaponData.basePoison * stats.GetFinalValue(StatType.Duration),
            slowDuration = weaponData.baseSlowdown * stats.GetFinalValue(StatType.Duration),
        };
    }

    public static DamageInfo GetDamageToPlayer(EnemyDefinitionSO enemyData)
    {
        StatSystem stats = ContextRegister.Instance.GetContext().statSystem;

        if (enemyData == null)
        {
            Debug.LogWarning("Attack Made has no enemyData");
        }

        DamageInfo damageInfo = enemyData.MakeDamageInfo();
        damageInfo.damage *= 1 + (stats.GetFinalValue(StatType.Armor) * -0.01f);
        return damageInfo;
    }
}

