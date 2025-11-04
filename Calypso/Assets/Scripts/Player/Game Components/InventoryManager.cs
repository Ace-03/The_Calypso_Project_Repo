using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerContext playerContext;
    private StatSystem statSystem;

    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;


    [SerializeField] private List<EquippedItemInstance> passiveItems;
    [SerializeField] private List<WeaponController> weapons;

    private void Start()
    {
        playerContext = ContextRegister.Instance.GetContext();
        statSystem = playerContext.statSystem;
    }

    private void OnEnable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
    }

    private void OnDisable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
    }

    private void ProcessSelectedReward(SelectedRewardPayload reward)
    {
        EquippedItemInstance existingInstance = passiveItems.Find(i => i.itemData == reward.option.itemData);

        if (existingInstance == null)
        {
            AddNewItem(reward);
        }
        else
        {
            UpgradeItem(existingInstance, reward.option.modifiers);
        }
    }

    private void AddNewItem(SelectedRewardPayload reward)
    {
        if (passiveItems.Count >= maxPassiveItems) return;

        string instanceID = reward.option.instanceID;
        PassiveItemSO itemData = reward.option.itemData;
        List<StatModifier> levelOneModifiers = reward.option.modifiers;

        EquippedItemInstance newItemInstance = new EquippedItemInstance(
            itemData,
            instanceID,
            levelOneModifiers);

        passiveItems.Add(newItemInstance);

        if (levelOneModifiers.Count > 0)
        {
            foreach (StatModifier modifier in levelOneModifiers)
            {
                statSystem.AddModifier(modifier.StatType, modifier);
            }
        }

        foreach (ItemEffectSO effect in itemData.itemBehaviors)
        {
            effect.OnAcquired(newItemInstance, playerContext);
        }
    }

    private void UpgradeItem(EquippedItemInstance itemInstance, List<StatModifier> newModifiers)
    {
        foreach (StatModifier modifier in itemInstance.modifiers)
        {
            statSystem.RemoveAllModifiersBySource(modifier.StatType, itemInstance.instanceID);
        }

        itemInstance.itemLevel = itemInstance.itemLevel + 1;
        itemInstance.modifiers = newModifiers;

        if (newModifiers.Count > 0)
        {
            foreach (StatModifier modifier in newModifiers)
            {
                statSystem.AddModifier(modifier.StatType, modifier);
            }
        }

        // Optional: Broadcast event that an item was upgraded
    }

    public List<EquippedItemInstance> GetUpgradeableItems()
    {
        return passiveItems.FindAll(i => i.itemLevel < i.itemData.maxLevel);
    }

    public List<EquippedItemInstance> GetAllPassiveItems()
    {
        return passiveItems;
    }

    public List<WeaponController> GetAllWeapons()
    {
        return weapons;
    }

    public EquippedItemInstance GetItem(PassiveItemSO data)
    {
        return passiveItems.Find(i => i.itemData == data);
    }

    public WeaponController GetWeapon(WeaponDefinitionSO data)
    {
        return weapons.Find(w => w.GetWeaponData() == data);
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
