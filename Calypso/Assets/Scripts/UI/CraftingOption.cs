using TMPro;
using UnityEngine;

public class CraftingOption : MonoBehaviour
{
    public WeaponRecipeSO weaponRecipe;
    [SerializeField] private OnCraftingAttemptEventSO craftingAttemptEvent;
    [SerializeField] private InfoPanelComponents infoPanelComponents;

    private void Start()
    {
        SetupInfoPanel();
    }

    public void SetupInfoPanel()
    {
        infoPanelComponents.weaponNameText.text = weaponRecipe.rewardWeapon.weaponName;
        infoPanelComponents.requirementsText.text = 
            $"--Requirements--\n" +
            $"Base Level: {weaponRecipe.craftingRequirements.baseLevel}\n" +
            $"Iron: {weaponRecipe.craftingRequirements.ironCost}\n" +
            $"Stone: {weaponRecipe.craftingRequirements.stoneCost}";
        infoPanelComponents.descriptionText.text = weaponRecipe.rewardWeapon.weaponDescription;
    }

    public void ShowInfoPanel()
    {
        infoPanelComponents.panelParent.SetActive(true);
    }

    public void HideInfoPanel()
    {
        infoPanelComponents.panelParent.SetActive(false);
    }

    public void OnTryCraftWeapon()
    {
        craftingAttemptEvent.Raise(new CraftingAttemptPayload
        {
            weaponRecipe = weaponRecipe
        });
    }

    [System.Serializable]
    public struct InfoPanelComponents
    {
        public GameObject panelParent;
        public TextMeshProUGUI weaponNameText;
        public TextMeshProUGUI requirementsText;
        public TextMeshProUGUI descriptionText;
    }
}


