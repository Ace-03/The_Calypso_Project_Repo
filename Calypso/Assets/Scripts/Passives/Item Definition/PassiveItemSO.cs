using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPassiveItem", menuName = "Scriptable Objects/PassiveItem")]
public class PassiveItemSO : ScriptableObject
{
    [Header("Identity & Display")]
    public string itemName = "New Item";
    public string description = "Basic Upgrade";
    public Sprite sprite;
    public int maxLevel;
    public float itemBaseValue;
    public ItemRarity rarity;

    [Header("Behavior Implementations")]
    public List<IItemEffectSO> itemBehavior;

    [Header("Stat Modifier Templates")]
    public List<StatModifierTemplate> modifierTemplates;

    [Header("Upgrade Configuration")]

    public StatType primaryStatType = StatType.MaxSpeed;

    // additional info could be placed here like evolution info, synergies, and costs
}