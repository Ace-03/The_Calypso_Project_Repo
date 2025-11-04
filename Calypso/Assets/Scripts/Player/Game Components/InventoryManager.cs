using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerContext playerContext;
    private RewardGenerator rewardGenerator;
    private StatSystem statSystem;

    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;


    [SerializeField] private List<EquippedItemInstance> passiveItems;
    [SerializeField] private List<WeaponController> weapons;

    private void Start()
    {
        playerContext = ContextRegister.Instance.GetContext();
        rewardGenerator = playerContext.rewardGenerator;
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

    private void AddNewItem(PassiveItemSO itemData)
    {
        if (passiveItems.Count >= maxPassiveItems) return;

        // 1. Generate unique Instance ID
        string instanceID = System.Guid.NewGuid().ToString();

        // 2. Roll Modifiers for Level 1 (Channel 1: Leveling Data)
        List<StatModifier> levelOneModifiers = rewardGenerator.RollModifiersForLevel(
            itemData,
            1,
            instanceID);

        // 3. Create and store the runtime instance
        EquippedItemInstance newItemInstance = new EquippedItemInstance(
            itemData,
            instanceID,
            levelOneModifiers);

        passiveItems.Add(newItemInstance);

        // 4. Apply Modifiers to StatSystem (Channel 1 Execution)
        if (levelOneModifiers.Count > 0)
        {
            foreach (StatModifier modifier in levelOneModifiers)
            {
                statSystem.AddModifier(modifier.StatType, modifier);
            }
        }

        // 5. Execute IItemEffect Hooks (Channel 2: Behavior)
        foreach (ItemEffectSO effect in itemData.itemBehaviors)
        {
            effect.OnAcquired(newItemInstance, playerContext);
        }
    }

    private void UpgradeItem(EquippedItemInstance itemInstance)
    {
        int newLevel = itemInstance.itemLevel + 1;
        string instanceID = itemInstance.instanceID;

        // --- CRITICAL ATOMIC REPLACEMENT PHASE ---

        // 1. REMOVE OLD Modifiers (CRITICAL STEP)
        // Removes all modifiers associated with the item's unique InstanceID
        foreach (StatModifier modifier in itemInstance.modifiers)
        {
            statSystem.RemoveAllModifiersBySource(modifier.StatType, instanceID);
        }

        // 2. Roll NEW Modifiers (Data update for Level N+1)
        List<StatModifier> newModifiers = rewardGenerator.RollModifiersForLevel(
            itemInstance.itemData,
            newLevel,
            instanceID);

        // 3. Update the runtime instance data
        itemInstance.itemLevel = newLevel;
        itemInstance.modifiers = newModifiers;

        // 4. APPLY NEW Modifiers (Execution)

        if (newModifiers.Count > 0)
        {
            foreach (StatModifier modifier in newModifiers)
            {
                statSystem.AddModifier(modifier.StatType, modifier);
            }
        }

        // Note: The IItemEffect hooks (Channel 2) remain untouched as the item is still active.
        // Optional: Broadcast event that an item was upgraded
    }

    public List<EquippedItemInstance> GetUpgradeableItems()
    {
        return passiveItems.FindAll(i => i.itemLevel < i.itemData.maxLevel);
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
