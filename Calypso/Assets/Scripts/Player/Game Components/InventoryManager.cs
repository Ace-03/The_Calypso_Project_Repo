using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerContext playerContext;
    private StatSystem statSystem;


    [Header("Weapon Events")]
    [SerializeField] private OnWeaponCraftedEventSO weaponCraftedEvent;
    [SerializeField] private OnWeaponsUpdatedEventSO weaponsUpdatedEvent;
    [SerializeField] private OnBlueprintCollectedEventSO blueprintCollectedEvent;

    [Header("Passive Item Events")]
    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private OnStatsUpdatedSO statsUpdatedEvent;
    [SerializeField] private OnUpdateHotBarSO updateHotBarEvent;


    [Header("List Info")]
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;
    [SerializeField] private List<EquippedItemInstance> passiveItems;
    [SerializeField] private List<WeaponDefinitionSO> weapons;
    [SerializeField] private List<string> weaponBlueprints;

    private void OnEnable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
        weaponCraftedEvent.RegisterListener(ProcessCraftedWeapon);
        blueprintCollectedEvent.RegisterListener(AddBlueprint);
    }

    private void OnDisable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
        weaponCraftedEvent.RegisterListener(ProcessCraftedWeapon);
        blueprintCollectedEvent.UnregisterListener(AddBlueprint);
    }

    private void Start()
    {
        playerContext = ContextRegister.Instance.GetContext();
        statSystem = playerContext.statSystem;
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

    private void AddBlueprint(BlueprintCollectedPayload payload)
    {
        if (weaponBlueprints.Contains(payload.weaponName))
            Debug.LogWarning($"Player has already Unlocked {payload.weaponName}");
        else
            weaponBlueprints.Add(payload.weaponName);
    }

    #region Getters

    public List<EquippedItemInstance> GetUpgradeableItems() => passiveItems.FindAll(i => i.itemLevel < i.itemData.maxLevel);
    public List<EquippedItemInstance> GetAllPassiveItems() => passiveItems;
    public List<WeaponDefinitionSO> GetAllWeapons() => weapons;
    public EquippedItemInstance GetItem(PassiveItemSO data) => passiveItems.Find(i => i.itemData == data);
    public WeaponDefinitionSO GetWeapon(WeaponDefinitionSO data) => weapons.Find(w => w == data);
    public List<string> GetBlueprints() => weaponBlueprints;

    #endregion

    #region Checks


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
        foreach (WeaponDefinitionSO weapon in weapons)
        {
            if (weapon == weaponToCheck) return true;
        }
        return false;
    }

    public bool IsNewItemSlotAvailable() => passiveItems.Count < maxPassiveItems;

    #endregion
}
