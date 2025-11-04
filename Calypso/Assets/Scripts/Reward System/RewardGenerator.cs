using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardGenerator : MonoBehaviour
{
    private InventoryManager inventoryManager;
    private PlayerContext playerContext;

    [SerializeField] private OnRewardRequestedEventSO requestEvent;
    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private OnRewardsGeneratedSO requestGeneratedEvent;

    [SerializeField] private PassiveItemSetSO passiveItemPool;
    [SerializeField] private int rewardGenerationCount = 3;


    private void Start()
    {
        playerContext = ContextRegister.Instance.GetContext();
        inventoryManager = playerContext.inventoryManager;
    }

    private void OnEnable()
    {
        requestEvent.RegisterListener(GenerateRewardOptions);
    }

    private void OnDisable()
    {
        requestEvent.UnregisterListener(GenerateRewardOptions);
    }

    private void GenerateRewardOptions(SelectedRewardPayload payload)
    {
        List<EquippedItemInstance> equippedItems = inventoryManager.GetUpgradeableItems();
        bool canAquireNewItem = inventoryManager.IsNewItemSlotAvailable();

        List<PassiveItemSO> possibleRewards = FilterRewards(equippedItems, canAquireNewItem);

        List<RewardOption> finalOptions = SelectWeightedOptions(possibleRewards, rewardGenerationCount);

        requestGeneratedEvent.Raise(new RewardOptionsPayload() { options = finalOptions });
    }

    private List<PassiveItemSO> FilterRewards(List<EquippedItemInstance> equipped, bool canAquireNew)
    {
        List<PassiveItemSO> selectableRewards = equipped.Select(item => item.itemData).ToList();

        if (canAquireNew)
        {
            foreach (PassiveItemSO item in passiveItemPool.Items)
            {
                if (!inventoryManager.HasItem(item))
                {
                    selectableRewards.Add(item);
                }
            }
        }
        return selectableRewards;
    }

    // Logic to select random Items based on weight defined by rarity
    private List<RewardOption> SelectWeightedOptions(List<PassiveItemSO> eligibleRewards, int count)
    {
        List<RewardOption> finalOptions = new List<RewardOption>();

        if (eligibleRewards.Count <= count)
        {
            foreach (PassiveItemSO item in eligibleRewards)
            {
                finalOptions.Add(new RewardOption { itemData = item });
            }
            return finalOptions;
        }

        // Create a temporary, mutable list to remove selected options and ensure uniqueness
        List<PassiveItemSO> pool = new List<PassiveItemSO>(eligibleRewards);

        for (int i = 0; i < count; i++)
        {
            float totalWeight = 0f;

            // Calculate the total weight of the remaining pool
            foreach (PassiveItemSO item in pool)
            {
                // Get the base weight from the item's rarity
                float itemWeight = RarityWeights.GetWeightMultiplier(item.rarity);

                // Apply contextual modifiers: UPGRADES are often prioritized.
                // A simple check: If the item is already equipped, it's an upgrade option, boost its weight.
                if (inventoryManager.HasItem(item))
                {
                    itemWeight *= 1.5f; // 50% boost to weight for upgrades
                }

                totalWeight += itemWeight;
            }

            // 2. Roll the Dice (The core random selection)
            float randomPoint = Random.Range(0f, totalWeight);

            // 3. Find the Winning Item
            PassiveItemSO selectedItem = null;

            for (int j = 0; j < pool.Count; j++)
            {
                PassiveItemSO currentItem = pool[j];
                float currentWeight = RarityWeights.GetWeightMultiplier(currentItem.rarity);
                if (inventoryManager.HasItem(currentItem))
                {
                    currentWeight *= 1.5f;
                }

                // Subtract the item's weight from the random point. 
                // The first item to make randomPoint <= 0 is the winner.
                if (randomPoint < currentWeight)
                {
                    selectedItem = currentItem;
                    break;
                }
                randomPoint -= currentWeight;
            }

            // 4. Finalize Selection
            if (selectedItem != null)
            {
                finalOptions.Add(new RewardOption { itemData = selectedItem });
                pool.Remove(selectedItem); // Remove the selected item to ensure uniqueness in this reward offer
            }
        }

        return finalOptions;
    }

    public List<StatModifier> RollModifiersForLevel(
    PassiveItemSO itemData,
    int level,
    string sourceInstanceID)
    {
        if (itemData.modifierTemplates == null) return null;

        List<StatModifier> newModifiers = new List<StatModifier>();

        foreach (StatModifierTemplate template in itemData.modifierTemplates)
        {
            float rolledValue = Random.Range(template.MinValue, template.MaxValue);

            float finalValue = rolledValue * level;

            var modifier = new StatModifier(
                template.Type,
                finalValue,
                template.ModType,
                sourceInstanceID
            );

            newModifiers.Add(modifier);
        }
        return newModifiers;
    }
}
