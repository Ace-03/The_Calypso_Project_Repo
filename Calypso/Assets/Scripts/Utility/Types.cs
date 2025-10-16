using UnityEngine;

public class PlayerHealthData : HealthData
{
    public float invulnerabilityDuration;
}
public class HealthData
{
    public int maxHP;
}

public struct DamageInfo
{
    public float damage;
    public float knockbackStrength;
    public float stunDuration;
    public float poisonDuration;
    public float slowDuration;
}

[System.Serializable]
public class ItemDrop
{
    public PickupSO data;
    public float dropChance;
    public int minAmount;
    public int maxAmount;
}

[System.Serializable]
public struct playerSprites
{
    public Sprite backSprite;
    public Sprite frontSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
}

[System.Serializable]
public struct playerRenderers
{
    public SpriteRenderer player;
    public SpriteRenderer weapon;
    public SpriteRenderer shadow;
}