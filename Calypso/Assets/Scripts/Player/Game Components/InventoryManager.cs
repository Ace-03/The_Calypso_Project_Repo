using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerContext playerContext;
    private StatSystem statSystem;

    [Header("Passive Item Events")]
    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private OnStatsUpdatedSO statsUpdatedEvent;
    [SerializeField] private OnUpdateHotBarSO updateHotBarEvent;
    
    [Header("Weapon Events")]
    [SerializeField] private OnWeaponCraftedEventSO weaponCraftedEvent;
    [SerializeField] private OnWeaponsUpdatedEventSO weaponsUpdatedEvent;

    [Header("List Info")]
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;
    [SerializeField] private List<EquippedItemInstance> passiveItems;
    [SerializeField] private List<WeaponDefinitionSO> weapons;

    private void Start()
    {
        playerContext = ContextRegister.Instance.GetContext();
        statSystem = playerContext.statSystem;
    }

    private void OnEnable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
        weaponCraftedEvent.RegisterListener(ProcessCraftedWeapon);
    }

    private void OnDisable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
        weaponCraftedEvent.RegisterListener(ProcessCraftedWeapon);
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

    private void ProcessCraftedWeapon(WeaponCraftedPayload payload)
    {
        if (weapons.Count >= maxWeapons)
        {
            Debug.LogWarning("Max weapon limit reached. Cannot add new weapon.");
            Debug.LogWarning("In the future add weapon Swapping System.");
            return;
        }

        if (HasWeapon(payload.weaponData))
        {
            Debug.LogWarning("Weapon already exists in inventory. Cannot add duplicate weapon.");
            return;
        }

        weapons.Add(payload.weaponData);

        List<PassiveItemSO> currentItems = passiveItems.Select(item => item.itemData).ToList();

        weaponsUpdatedEvent.Raise(new WeaponsUpdatePayload() { weapons = weapons });
        updateHotBarEvent.Raise(new UpdateHotBarPayload(weapons, currentItems));
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

        List<PassiveItemSO> currentItems = passiveItems.Select(item => item.itemData).ToList();

        statsUpdatedEvent.Raise(new StatUpdatePayload(statSystem));
        updateHotBarEvent.Raise(new UpdateHotBarPayload(weapons, currentItems));
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

        statsUpdatedEvent.Raise(new StatUpdatePayload(statSystem));
    }

    public List<EquippedItemInstance> GetUpgradeableItems()
    {
        return passiveItems.FindAll(i => i.itemLevel < i.itemData.maxLevel);
    }

    public List<EquippedItemInstance> GetAllPassiveItems()
    {
        return passiveItems;
    }

    public List<WeaponDefinitionSO> GetAllWeapons()
    {
        return weapons;
    }

    public EquippedItemInstance GetItem(PassiveItemSO data)
    {
        return passiveItems.Find(i => i.itemData == data);
    }

    public WeaponDefinitionSO GetWeapon(WeaponDefinitionSO data)
    {
        return weapons.Find(w => w == data);
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
        foreach(WeaponDefinitionSO weapon in weapons)
        {
            if (weapon == weaponToCheck) return true;
        }
        return false;
    }

    public bool IsNewItemSlotAvailable()
    {
        return passiveItems.Count < maxPassiveItems;
    }
}
