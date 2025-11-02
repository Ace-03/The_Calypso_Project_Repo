using System;
using UnityEngine;

[Serializable]
public class EquippedItemInstance
{
    public PassiveItemSO itemData;
    public int itemLevel;
    public float itemValue;
    public string instanceID;

    public int GetItemLevel()
    {
        return itemLevel;
    }

    public void LevelUp(float valueIncrease)
    {
        itemLevel++;
        itemValue += valueIncrease;
    }

    public EquippedItemInstance(PassiveItemSO data, string instanceId)
    {
        itemData = data;
        itemValue = data.itemBaseValue;
        itemLevel = 1;
        instanceID = instanceId;
    }
}
