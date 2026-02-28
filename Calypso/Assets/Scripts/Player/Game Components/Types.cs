using System;
using System.Collections.Generic;
using UnityEngine;

public class StatContainer
{
    public float BaseValue;
    public float FinalValue;
    public List<StatModifier> Modifiers;
}

[Serializable]
public class StatModifier
{
    public readonly StatType StatType;
    public readonly float Value;
    public readonly string SourceID;
    public readonly StatModifierType ModType;
    
    public StatModifier(StatType type, float value, StatModifierType modType, string sourceId)
    {
        StatType = type;
        Value = value;
        SourceID = sourceId;
        ModType = modType;
    }

    public void DebugModifier()
    {
        Debug.Log($"Stat Type: {StatType} \n" +
            $"Value: {Value}\n" +
            $"sourceId: {SourceID}\n" +
            $"Modifier Type: {ModType}\n");
    }
}

[Serializable]
public struct StatModifierTemplate
{
    public StatType Type;
    public StatModifierType ModType;
    public float MinValue;
    public float MaxValue;
}

public enum StatModifierType
{
    FlatAdd,
    AdditivePercentage,
    MultPercentage
}

public enum StatType
{
    MaxHealth,
    Armor,
    Recovery,
    Invulnerability,
    Luck,
    MaxSpeed,
    Accel,
    Decel,
    Strength,
    Dexterity,
    Size,
    Cooldown,
    Duration,
    Amount,
    ItemAttraction,
    none,
}
