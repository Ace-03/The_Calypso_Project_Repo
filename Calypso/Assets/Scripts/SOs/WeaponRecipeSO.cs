using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponRecipe", menuName = "Progression/Weapon Recipe")]
public class WeaponRecipeSO : ScriptableObject
{
    public WeaponDefinitionSO rewardWeapon;
    public WeaponCraftingRequirements craftingRequirements;
}

[System.Serializable]
public class WeaponCraftingRequirements
{
    public int ironCost;
    public int stoneCost;
    public int baseLevel;
}