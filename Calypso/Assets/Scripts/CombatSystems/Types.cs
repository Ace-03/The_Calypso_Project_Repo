using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    public float Cooldown;
    public float Damage;
    public int Amount;
    public float Duration;
    public float accuracy;
    public float Speed;
    public float AOETick;
    public float Area;
}

public class DamageSource
{
    public WeaponDefinitionSO weapon;
    public GameObject sourceObject;
    public EnemyDefinitionSO enemyDefinition;

    public DamageSource(WeaponDefinitionSO wpn = null, 
        GameObject srcObj = null, 
        EnemyDefinitionSO enemyDef = null)
    {
        weapon = wpn;
        sourceObject = srcObj;
        enemyDefinition = enemyDef;
    }
}


/* public method in enemy data used to calculate damage values to player. this should be calculated and sent to
 * 
    public DamageInfo MakeDamageInfo()
    {
        return new DamageInfo
        {
            damage = weapon.baseStats.Damage * DifficultyModifier,
            knockbackStrength = weapon.baseKnockback * DifficultyModifier,
            stunDuration = weapon.baseStun * DifficultyModifier,
            poisonDuration = weapon.basePoison * DifficultyModifier,
            slowDuration = weapon.baseSlowdown * DifficultyModifier,
        };
    }
*/
