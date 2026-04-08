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
