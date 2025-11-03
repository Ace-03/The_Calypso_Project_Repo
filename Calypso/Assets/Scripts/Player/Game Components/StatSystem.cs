using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class StatSystem : MonoBehaviour
{
    private Dictionary<StatType, StatContainer> _stats = new Dictionary<StatType, StatContainer>();

    private void Awake()
    {
        InitializeStat(StatType.MaxSpeed, 5f);
        InitializeStat(StatType.MaxHealth, 100f);
    }

    private void InitializeStat(StatType type, float baseValue)
    {
        _stats.Add(type, new StatContainer { BaseValue = baseValue, Modifiers = new List<StatModifier>() });
    }

    public void AddModifier(StatType type, StatModifier modifier)
    {
        if (_stats.TryGetValue(type, out StatContainer container))
        {
            container.Modifiers.Add(modifier);
            CalculateFinalValue(type);
        }
    }

    public void RemoveAllModifiersBySource(StatType type, string sourceId)
    {
        if (_stats.TryGetValue(type, out StatContainer container))
        {
            container.Modifiers.RemoveAll(mod => mod.SourceID == sourceId);
            CalculateFinalValue(type);
        }
    }

    public float GetFinalValue(StatType type)
    {
        if (_stats.TryGetValue(type, out StatContainer container))
        {
            return container.FinalValue;
        }
        return 0f;
    }

    private void CalculateFinalValue(StatType type)
    {
        if (!_stats.TryGetValue(type, out StatContainer container)) return;

        float finalValue = container.BaseValue;

        float flatAdd = container.Modifiers
            .Where(m => m.ModType == StatModifierType.FlatAdd)
            .Sum(m => m.Value);
        finalValue += flatAdd;

        float percentAddSum = container.Modifiers
            .Where(m => m.ModType == StatModifierType.AdditivePercentage)
            .Sum(m => m.Value);
        finalValue *= (1 + percentAddSum);

        foreach (var mod in container.Modifiers.Where(m => m.ModType == StatModifierType.MultPercentage))
        {
            finalValue *= (1 + mod.Value);
        }

        container.FinalValue = Mathf.Max(0f, finalValue); 
    }
}
