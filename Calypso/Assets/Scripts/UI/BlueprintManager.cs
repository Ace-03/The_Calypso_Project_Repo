using System.Collections.Generic;
using UnityEngine;

public class BlueprintManager : MonoBehaviour
{
    public static BlueprintManager Instance;

    public List<CraftingOption> craftingOptions = new List<CraftingOption>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }


    public void CheckRecipes()
    {
        // Crafting option objects start asleep.
        // if no options are seen then delay and try again.
        if (craftingOptions.Count <= 0)
        {
            Invoke(nameof(CheckRecipes), 0.1f);
            return;
        }

        Debug.Log("Checking recipes");

        InventoryManager invManager = ContextRegister.Instance.GetContext().inventoryManager;
        List<string> blueprints = invManager.GetBlueprints();

        foreach (CraftingOption option in craftingOptions)
        {
            string weaponName = option.weaponRecipe.rewardWeapon.weaponName;
            bool isLocked = !blueprints.Contains(weaponName);

            Debug.Log($"Setting {weaponName} to {isLocked}");

            option.LockCraftingOption(isLocked);
        }
    }
}
