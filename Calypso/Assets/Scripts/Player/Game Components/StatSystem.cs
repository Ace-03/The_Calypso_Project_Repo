using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StatSystem : MonoBehaviour
{
    [SerializeField] private PlayerBaseStatsSO baseStats;
    private Dictionary<StatType, StatContainer> stats = new Dictionary<StatType, StatContainer>();

    private void Awake()
    {
        InitializeStat(StatType.MaxHealth, baseStats.maxHealth);
        InitializeStat(StatType.Armor, baseStats.armor);
        InitializeStat(StatType.Recovery, baseStats.recovery);
        InitializeStat(StatType.Invulnerability, baseStats.invulnerabilityPeriod);
        InitializeStat(StatType.Luck, baseStats.luck);
        InitializeStat(StatType.MaxSpeed, baseStats.maxSpeed);
        InitializeStat(StatType.Strength, baseStats.strength);
        InitializeStat(StatType.Dexterity, baseStats.dexterity);
        InitializeStat(StatType.Size, baseStats.size);
        InitializeStat(StatType.Cooldown, baseStats.cooldown);
        InitializeStat(StatType.Duration, baseStats.duration);
        InitializeStat(StatType.Amount, baseStats.ammount);

        InitializeStat(StatType.Accel, baseStats.acceleration);
        InitializeStat(StatType.Decel, baseStats.deceleration);
    }

    private void InitializeStat(StatType type, float baseValue)
    {
        stats.Add(type, new StatContainer { BaseValue = baseValue, Modifiers = new List<StatModifier>() });
        CalculateFinalValue(type);
    }

    public void AddModifier(StatType type, StatModifier modifier)
    {
        if (type == StatType.none) return;

        if (stats.TryGetValue(type, out StatContainer container))
        {
            container.Modifiers.Add(modifier);
            CalculateFinalValue(type);
        }
    }

    public void RemoveAllModifiersBySource(StatType type, string sourceId)
    {
        if (stats.TryGetValue(type, out StatContainer container))
        {
            container.Modifiers.RemoveAll(mod => mod.SourceID == sourceId);
            CalculateFinalValue(type);
        }
    }

    public float GetFinalValue(StatType type)
    {
        if (stats.TryGetValue(type, out StatContainer container))
        {
            return container.FinalValue;
        }
        return 0f;
    }

    private void CalculateFinalValue(StatType type)
    {
        if (!stats.TryGetValue(type, out StatContainer container)) return;

        float finalValue = container.BaseValue;

        float flatAdd = container.Modifiers
            .Where(m => m.ModType == StatModifierType.FlatAdd)
            .Sum(m => m.Value);
        finalValue += flatAdd;

        float percentAddSum = container.Modifiers
            .Where(m => m.ModType == StatModifierType.AdditivePercentage)
            .Sum(m => m.Value);
        finalValue *= (1 + (percentAddSum / 100));

        foreach (StatModifier mod in container.Modifiers.Where(m => m.ModType == StatModifierType.MultPercentage))
        {
            finalValue *= (1 + (mod.Value / 100));
        }

        container.FinalValue = Mathf.Max(0f, finalValue); 
    }

    public void DebugStatAndModifiers(StatType type)
    {
        Debug.Log($"Debugging Stat: {type}");
        Debug.Log($"Base value of stat {type} is: {stats[type].BaseValue}");
        Debug.Log($"Final value of stat {type} is: {GetFinalValue(type)}");
        
        if (stats.TryGetValue(type, out StatContainer container))
        {
            foreach (var mod in container.Modifiers)
            {
                Debug.Log($"Modifier - Type: {mod.ModType}, Value: {mod.Value}, SourceID: {mod.SourceID}");
            }
        }
    }
}
