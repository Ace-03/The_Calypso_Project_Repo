using System;

[Serializable]
public struct StatModifier
{
    public float Value;
    public string StatName;
    public object Source;

    public ModifierType type;
}

public enum ModifierType
{
    Flat,
    AdditivePercentage,
    MultiplicitavePercentage
}