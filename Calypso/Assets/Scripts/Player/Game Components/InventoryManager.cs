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

    private void ProcessSelectedReward(RewardOption option)
    {
        EquippedItemInstance existingInstance = passiveItems.Find(i => i.GetItemData() == option.itemData);

        if (existingInstance == null)
        {
            AddNewItem(option.itemData);
        }
        else
        {
            UpgradeItem(existingInstance, option.itemValueIncrease);
        }
    }

    private void AddNewItem(PassiveItemSO item)
    {
        EquippedItemInstance newItem = new EquippedItemInstance(item);
        passiveItems.Add(newItem);

        newItem.GetItemData().itemBehavior.OnAquired(newItem, ContextRegister.Instance.GetContext());
    }

    private void UpgradeItem(EquippedItemInstance item, float value)
    {
        item.LevelUp(value);
    }

    public List<EquippedItemInstance> GetPassiveItems()
    {
        return passiveItems;
    }

    public List<WeaponController> GetWeapons()
    {
        return weapons;
    }

    public bool IsNewItemSlotAvailable()
    {
        return passiveItems.Count < maxPassiveItems;
    }
}
