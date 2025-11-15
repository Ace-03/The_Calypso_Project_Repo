using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EquippedItemInstance
{
    public PassiveItemSO itemData;
    public int itemLevel;
    public string instanceID { get; private set; }

    public List<StatModifier> modifiers;


    public EquippedItemInstance(PassiveItemSO data, string instanceId, List<StatModifier> LevelOneModifiers)
    {
        itemData = data;
        itemLevel = 1;
        instanceID = instanceId;
        modifiers = LevelOneModifiers;
    }
}
