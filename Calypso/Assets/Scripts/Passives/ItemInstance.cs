using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInstance
{
    public PassiveItemSO itemData;
    public int itemLevel;
    public string instanceID { get; private set; }
    public bool permanentlyOwned = false;

    public List<StatModifier> modifiers;

    public ItemInstance(PassiveItemSO data, 
        string instanceId, 
        List<StatModifier> LevelOneModifiers, 
        bool owned)
    {
        itemData = data;
        itemLevel = 1;
        instanceID = instanceId;
        modifiers = LevelOneModifiers;
        permanentlyOwned = owned;
    }

    public void DebugModifiers()
    {
        Debug.Log($"Printing Modifiers for {itemData.itemName}");

        foreach (StatModifier modifier in modifiers)
            modifier.DebugModifier();
    }
}
