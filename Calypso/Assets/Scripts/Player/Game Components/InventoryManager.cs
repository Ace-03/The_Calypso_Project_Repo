using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;

    [SerializeField] private List<EquippedItemInstance> passiveItems;
    [SerializeField] private List<WeaponController> weapons;

    private void OnEnable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
    }

    private void OnDisable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
    }

    private void ProcessSelectedReward(SelectedRewardPayload option)
    {
        EquippedItemInstance existingInstance = passiveItems.Find(i => i.itemData == option.itemData);

        if (existingInstance == null)
        {
            AddNewItem(option.itemData);
        }
        else
        {
            UpgradeItem(existingInstance);
        }
    }

    private void AddNewItem(PassiveItemSO item)
    {
        // EquippedItemInstance newItem = new EquippedItemInstance(item);
        // passiveItems.Add(newItem);

        // newItem.GetItemData().itemBehavior.OnAquired(newItem, ContextRegister.Instance.GetContext());
    }

    private void UpgradeItem(EquippedItemInstance item)
    {
        //item.LevelUp(value);
    }

    public List<EquippedItemInstance> GetPassiveItems()
    {
        return passiveItems;
    }

    public List<WeaponController> GetWeapons()
    {
        return weapons;
    }

    public bool HasItem(PassiveItemSO itemToCheck)
    {
        foreach (EquippedItemInstance equippedItem in passiveItems)
        {
            if (equippedItem.itemData == itemToCheck) return true;
        }
        return false;
    }

    public bool HasWeapon(WeaponDefinitionSO weaponToCheck)
    {
        foreach(WeaponController weapon in weapons)
        {
            if (weapon.GetWeaponData() == weaponToCheck) return true;
        }
        return false;
    }

    public bool IsNewItemSlotAvailable()
    {
        return passiveItems.Count < maxPassiveItems;
    }
}
