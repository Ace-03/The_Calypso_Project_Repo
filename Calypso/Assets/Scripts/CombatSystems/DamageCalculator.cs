using UnityEngine;

public static class DamageCalculator
{
    public static DamageInfo CalculateDamageToEnemy(DamageSource src)
    {
        StatSystem stats = ContextRegister.Instance.GetContext().statSystem;
        WeaponDefinitionSO weaponData = src.weapon;

        return new DamageInfo
        {
            damage = weaponData.baseStats.Damage * stats.GetFinalValue(StatType.Strength),
            knockbackStrength = weaponData.baseKnockback * stats.GetFinalValue(StatType.Strength),
            stunDuration = weaponData.baseStun * stats.GetFinalValue(StatType.Dexterity),
            poisonDuration = weaponData.basePoison * stats.GetFinalValue(StatType.Duration),
            slowDuration = weaponData.baseSlowdown * stats.GetFinalValue(StatType.Duration),
        };
    }

    public static DamageInfo CalculateDamageToPlayer(DamageSource src)
    {
        EnemyDefinitionSO enemyData = src.enemyDefinition;

        StatSystem stats = ContextRegister.Instance.GetContext().statSystem;

        if (enemyData == null)
        {
            Debug.LogWarning("Attack Made has no enemyData");
            Debug.LogWarning($"source object is {src.sourceObject}");
        }

        DamageInfo damageInfo = enemyData.MakeDamageInfo();
        damageInfo.damage *= 1 + (stats.GetFinalValue(StatType.Armor) * -0.01f);
        return damageInfo;
    }
}

