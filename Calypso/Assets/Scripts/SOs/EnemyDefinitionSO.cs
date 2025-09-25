using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDefinition", menuName = "EnemySO")]
public class EnemyDefinitionSO : ScriptableObject
{
    public string enemyName;
    public Sprite icon;
    public GameObject enemyPrefab;
    public GameObject deathEffect;
    public int maxHealth;
    public float movementSpeed;
    public float DifficultyModifier;
    internal float aimSpeed;
    public WeaponDefinitionSO weapon;
    public float knockbackResistance;
    public float stunResistance;
    public float poisonResistance;
    public float slowResistance;
    public int experienceReward;
    public int resource1;
    public int resource2;

    public DamageInfo MakeDamageInfo()
    {
        return new DamageInfo
        {
            damage = weapon.baseDamage * DifficultyModifier,
            knockbackStrength = weapon.baseKnockback * DifficultyModifier,
            stunDuration = weapon.baseStun * DifficultyModifier,
            poisonDuration = weapon.basePoison * DifficultyModifier,
            slowDuration = weapon.baseSlowdown * DifficultyModifier,
        };
    }
}
