public static class RarityWeights
{
    public static float GetWeightMultiplier(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.common: return 50f;
            case ItemRarity.rare: return 20f;
            case ItemRarity.epic: return 8f;
            case ItemRarity.legendary: return 1f;
            default: return 0f;
        }
    }
}

public class RewardOption
{
    public PassiveItemSO itemData;
}

public enum ItemRarity
{
    common,
    rare,
    epic,
    legendary
}