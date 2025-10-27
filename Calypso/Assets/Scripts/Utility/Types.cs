using System;
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

[Serializable]
public class ItemDrop
{
    public PickupSO data;
    public float dropChance;
    public int minAmount;
    public int maxAmount;
}

[Serializable]
public struct spriteControllerData
{
    public playerRenderers renderers;
    public playerSprites player;
    public weaponSprites weapon;
}

[Serializable]
public struct playerSprites
{
    public Sprite backSprite;
    public Sprite frontSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
}

[Serializable]
public struct weaponSprites
{
    public Sprite forwardSprite;
    public Sprite sideSprite;
}

[Serializable]
public struct playerRenderers
{
    public SpriteRenderer player;
    public SpriteRenderer weapon;
    public SpriteRenderer shadow;
}

[Serializable]
public class DayCycleData
{
    public Light light;
    public Transform lightPivot;

    [Range(0, 360)] public float dayStartAngle;
    [Range(0, 360)] public float nightStartAngle;

    public Color sunriseColor;
    public Color daylightColor;
    public Color sunsetColor;
    public Color nightColor;

    public float sunriseIntensity;
    public float dayIntensity;
    public float sunsetIntensity;
    public float nightIntensity;

    public float sunriseShadow;
    public float dayShadow;
    public float sunsetShadow;
    public float nightShadow;
}

public enum LightingState
{
    sunrise,
    morning,
    daylight,
    evening,
    sunset,
    night,
}

public enum Rarity
{
    common,
    uncommon,
    rare,
    epic,
    legendary
}