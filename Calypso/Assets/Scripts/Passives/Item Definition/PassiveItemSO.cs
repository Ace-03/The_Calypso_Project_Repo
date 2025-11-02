using UnityEngine;

public abstract class PassiveItemSO : ScriptableObject
{
    public string itemName = "New Item";
    public string description = "Basic Upgrade";
    public Sprite sprite;
    public int maxLevel;
    public float itemBaseValue;
    public IItemEffect itemBehavior;
    public ItemRarity rarity;
}