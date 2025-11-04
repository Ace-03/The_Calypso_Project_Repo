using System;
using System.Collections.Generic;

public class StatContainer
{
    public float BaseValue;
    public float FinalValue;
    public List<StatModifier> Modifiers;
}

public readonly struct StatModifier
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
    Strength,
    Dexterity,
    Size,
    Cooldown,
    Duration,
    Amount,
    none,

    Accel,
    Decel,
}
