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