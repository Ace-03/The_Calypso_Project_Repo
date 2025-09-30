using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDefinition", menuName = "EnemySO")]
public class EnemyDefinitionSO : ScriptableObject
{
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
    public float knockbackResistance;
    public float stunResistance;
    public float poisonResistance;
    public float slowResistance;

    [System.Serializable]
    public class ItemDrop
    {
        public PickupSO data;
        public float dropChance;
        public int minAmount;
        public int maxAmount;
    }

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
