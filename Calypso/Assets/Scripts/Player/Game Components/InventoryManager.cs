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
    [SerializeField] private OnGenericEventSO passiveRestrictionSelected;

    [Header("List Info")]
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;
    [SerializeField] private List<ItemInstance> passiveItems;
    [SerializeField] private List<WeaponDefinitionSO> weapons;
    [SerializeField] private List<string> weaponBlueprints;

    private void OnEnable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
        weaponCraftedEvent.RegisterListener(ProcessCraftedWeapon);
        blueprintCollectedEvent.RegisterListener(AddBlueprint);
        passiveRestrictionSelected.RegisterListener(ClearPassivesNotPermanentlyOwned);
    }

    private void OnDisable()
    {
        rewardSelectedEvent.UnregisterListener(ProcessSelectedReward);
        weaponCraftedEvent.UnregisterListener(ProcessCraftedWeapon);
        blueprintCollectedEvent.UnregisterListener(AddBlueprint);
        passiveRestrictionSelected.UnregisterListener(ClearPassivesNotPermanentlyOwned);
    }

    private void Start()
    {
        playerContext = ContextRegister.Instance.GetContext();
        statSystem = playerContext.statSystem;
    }

    private void ProcessSelectedReward(SelectedRewardPayload reward)
    {
        ItemInstance existingInstance = passiveItems.Find(i => i.itemData == reward.option.itemData);

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

        RaiseUpdateHotbarEvent();
        weaponsUpdatedEvent.Raise(new WeaponsUpdatePayload() { weapons = weapons });
    }

    private void AddNewItem(SelectedRewardPayload reward)
    {
        if (passiveItems.Count >= maxPassiveItems)
        {
            Debug.LogWarning($"Player has reached max item count aborting passive addition");
            return;
        }

        string instanceID = reward.option.instanceID;
        PassiveItemSO itemData = reward.option.itemData;
        List<StatModifier> levelOneModifiers = reward.option.modifiers;

        ItemInstance newItemInstance = new ItemInstance(
            itemData,
            instanceID,
            levelOneModifiers,
            false
        );

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

        RaiseUpdateHotbarEvent();
        statsUpdatedEvent.Raise(new StatUpdatePayload(statSystem));
    }

    private void UpgradeItem(ItemInstance itemInstance, List<StatModifier> newModifiers)
    {
        foreach (StatModifier modifier in itemInstance.modifiers)
        {
            statSystem.RemoveAllModifiersBySource(modifier.StatType, itemInstance.instanceID);
        }

        itemInstance.itemLevel = itemInstance.itemLevel + 1;
        itemInstance.modifiers = newModifiers;

        Debug.Log("In upgrade Item, the new modifiers provided will be printed below");
        foreach (StatModifier mod in newModifiers)
            mod.DebugModifier();

        if (newModifiers.Count > 0)
        {
            foreach (StatModifier modifier in newModifiers)
            {
                statSystem.AddModifier(modifier.StatType, modifier);
            }
        }

        itemInstance.DebugModifiers();
        statsUpdatedEvent.Raise(new StatUpdatePayload(statSystem));
    }

    private void AddBlueprint(BlueprintCollectedPayload payload)
    {
        if (weaponBlueprints.Contains(payload.weaponName))
            Debug.LogWarning($"Player has already Unlocked {payload.weaponName}");
        else
            weaponBlueprints.Add(payload.weaponName);
    }

    private void ClearPassivesNotPermanentlyOwned(GameEventPayload payload)
    {
        foreach (ItemInstance item in passiveItems)
        {
            foreach (StatModifier modifier in item.modifiers)
            {
                statSystem.RemoveAllModifiersBySource(modifier.StatType, item.instanceID);
            }
        }

        passiveItems.RemoveAll(item => !item.permanentlyOwned);

        foreach (ItemInstance item in passiveItems)
        {
            foreach (StatModifier modifier in item.modifiers)
            {
                statSystem.AddModifier(modifier.StatType, modifier);
            }
        }

        RaiseUpdateHotbarEvent();
    }

    private void RaiseUpdateHotbarEvent()
    {
        List<PassiveItemSO> currentItems = passiveItems.Select(item => item.itemData).ToList();
        updateHotBarEvent.Raise(new UpdateHotBarPayload(weapons, currentItems));
    } 

    #region Getters

    public List<ItemInstance> GetUpgradeableItems() => passiveItems.FindAll(i => i.itemLevel < i.itemData.maxLevel);
    public List<ItemInstance> GetAllPassiveItems() => passiveItems;
    public List<WeaponDefinitionSO> GetAllWeapons() => weapons;
    public ItemInstance GetItem(PassiveItemSO data) => passiveItems.Find(i => i.itemData == data);
    public WeaponDefinitionSO GetWeapon(WeaponDefinitionSO data) => weapons.Find(w => w == data);
    public List<string> GetBlueprints() => weaponBlueprints;

    #endregion

    #region Checks

    public bool HasItem(PassiveItemSO itemToCheck)
    {
        foreach (ItemInstance equippedItem in passiveItems)
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

    public bool NewPassiveInPossession()
    {
        foreach (ItemInstance item in passiveItems)
            if (!item.permanentlyOwned) return true;

        return false;
    }

    #endregion
}
