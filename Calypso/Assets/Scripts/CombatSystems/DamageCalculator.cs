using UnityEngine;

public static class DamageCalculator
{
    public static DamageInfo CalculateDamageToEnemy(DamageSource src)
    {
        StatSystem stats = ContextRegister.Instance.GetContext().statSystem;

        Debug.Assert(src != null, "Attack To Enemy has No Damage Source");
        Debug.Assert(src.weapon != null, "Attack To Enemy has No Weapon");
        Debug.Assert(src.sourceObject != null, "Attack To Enemy has No Source Object");

        return MakeEnemyDamageInfo(src, stats);
    }

    public static DamageInfo CalculateDamageToPlayer(DamageSource src)
    {
        EnemyDefinitionSO enemyData = src.enemyDefinition;

        StatSystem stats = ContextRegister.Instance.GetContext().statSystem;

        Debug.Assert(src != null, $"Attack To Player has No Damage Source");
        Debug.Assert(src.sourceObject != null, $"Attack To Player has No Source Object");
        Debug.Assert(src.enemyDefinition != null, $"Attack To Player has No Enemy Data, Source Object is {src.sourceObject}");
        Debug.Assert(src.weapon != null, $"Attack To Player has No Weapon, Source Object is {src.sourceObject}");

        DamageInfo damageInfo = MakePlayerDamageInfo(src);
        damageInfo.damage *= 1 + (stats.GetFinalValue(StatType.Armor) * -0.01f);
        return damageInfo;
    }

    private static DamageInfo MakeEnemyDamageInfo(DamageSource src, StatSystem stats)
    {
        WeaponDefinitionSO weaponData = src.weapon;
        EnemyDefinitionSO enemy = src.enemyDefinition;

        float baseDamage = weaponData.baseStats.Damage * stats.GetFinalValue(StatType.Strength);
        float baseKnockback = weaponData.baseKnockback * stats.GetFinalValue(StatType.Strength);
        float baseStun = weaponData.baseStun * stats.GetFinalValue(StatType.Dexterity);
        float basePoison = weaponData.basePoison * stats.GetFinalValue(StatType.Duration);
        float baseSlow = weaponData.baseSlowdown * stats.GetFinalValue(StatType.Duration);

        return new DamageInfo
        {
            damage = baseDamage,
            knockbackStrength = baseKnockback,
            stunDuration = baseStun,
            poisonDuration = basePoison,
            slowDuration = baseSlow,
        };
    }

    private static DamageInfo MakePlayerDamageInfo(DamageSource src)
    {
        WeaponDefinitionSO weapon = src.weapon;
        EnemyDefinitionSO enemy = src.enemyDefinition;

        return new DamageInfo
        {
            damage = weapon.baseStats.Damage * enemy.DifficultyModifier,
            knockbackStrength = weapon.baseKnockback *enemy. DifficultyModifier,
            stunDuration = weapon.baseStun * enemy.DifficultyModifier,
            poisonDuration = weapon.basePoison * enemy.DifficultyModifier,
            slowDuration = weapon.baseSlowdown * enemy.DifficultyModifier,
        };
    }
}

