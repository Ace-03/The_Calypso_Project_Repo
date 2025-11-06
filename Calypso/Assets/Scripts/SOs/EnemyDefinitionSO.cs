using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDefinition", menuName = "Scriptable Objects/Enemy")]
public class EnemyDefinitionSO : ScriptableObject
{
    [Header("Main")]
    public string enemyName;
    public Sprite sprite;
    public Color spriteColor = Color.white;
    public float sizeModifier = 1f;
    public GameObject enemyPrefab;
    public GameObject deathEffect;
    public int maxHealth;
    public float movementSpeed;
    public float DifficultyModifier;
    public float aimSpeed;
    public WeaponDefinitionSO weapon;

    [Header("Status")]
    public float knockbackResistance;
    public float stunResistance;
    public float poisonResistance;
    public float slowResistance;

    [Header("Visual")]
    public float bobAmount;
    public float bobSpeed;

    public List<ItemDrop> possibleDrops;

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
