using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingOption : MonoBehaviour
{
    public WeaponRecipeSO weaponRecipe;

    [Header("Components")]
    [SerializeField] private OnCraftingAttemptEventSO craftingAttemptEvent;
    [SerializeField] private InfoPanelComponents infoPanelComponents;
    [SerializeField] private Button craftingButton;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image weaponIcon;

    [Header("Locked Properties")]
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color UnlockedColor;
    [Range(0,1)][SerializeField] private float lockedAlpha;

    private void Start()
    {
        SetupInfoPanel();
        weaponIcon.sprite = weaponRecipe.rewardWeapon.icon;
        BlueprintManager.Instance.craftingOptions.Add(this);
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

    public void LockCraftingOption(bool locked)
    {
        craftingButton.enabled = !locked;

        if (locked)
        {
            weaponIcon.color = lockedColor;
            Color bc = buttonImage.color;
            buttonImage.color = new Color(bc.r, bc.g, bc.b, lockedAlpha);
        }
        else
        {
            weaponIcon.color = UnlockedColor;
            Color bc = buttonImage.color;
            buttonImage.color = new Color(bc.r, bc.g, bc.b, 1);
        }
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


