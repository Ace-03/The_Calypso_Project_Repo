using System;
using UnityEngine;

[Serializable]
public class EquippedItemInstance
{
    private int itemLevel;
    private float itemValue;
    private PassiveItemSO itemData;

    public EquippedItemInstance(PassiveItemSO data)
    {
        itemData = data;
        itemValue = data.itemBaseValue;
    }

    public PassiveItemSO GetItemData()
    {
        return itemData;
    }

    public int GetItemLevel()
    {
        return itemLevel;
    }

    public void LevelUp(float valueIncrease)
    {
        itemLevel++;
        itemValue += valueIncrease;
    }
}
