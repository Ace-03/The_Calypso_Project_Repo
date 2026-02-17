using System;
using TMPro;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private OnCraftingAttemptEventSO craftingAttemptEvent;
    [SerializeField] private OnWeaponCraftedEventSO weaponCraftedEvent;

    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject BaseLevelUpScreen;
    [SerializeField] private GameObject WeaponCraftingScreen;
    [SerializeField] private GameObject PrimaryWeaponUpgradeScreen;
    [SerializeField] private GameObject MainBaseScreen;
    [SerializeField] private TextMeshProUGUI statsTextBox;

    [SerializeField] private UpgradeInfoUI baseUpgradeUI;
    [SerializeField] private UpgradeInfoUI weaponUpgradeUI;

    private BaseManager baseManager;
    private PlayerPrimaryWeaponManager weaponManager;
    private ResourceTracker resourceTracker;
    private PlayerLevelManager levelManager;

    [HideInInspector]
    public bool isOpen = false;

    private void OnEnable()
    {
        craftingAttemptEvent.RegisterListener(OnTryCraftWeapon);
    }

    private void OnDisable()
    {
        craftingAttemptEvent.UnregisterListener(OnTryCraftWeapon);
    }

    private void Start()
    {
        PlayerContext context = ContextRegister.Instance.GetContext();

        resourceTracker = context.resourceTracker;
        levelManager = context.levelManager;
        weaponManager = context.primaryWeaponManager;
        baseManager = context.baseManager;
    }

    public void OnMainMenu()
    {
        SetScreen(MainBaseScreen);
        HideCurrentStats();
    }

    public void OnCraftingScreen()
    {
        SetScreen(WeaponCraftingScreen);
        DisplayCurrentStats();
        BlueprintManager.Instance.CheckRecipes();
    }

    public void OnPrimaryWeaponScreen()
    {
        SetScreen(PrimaryWeaponUpgradeScreen);
        UpdateUpgradeInfo(weaponManager.GetCurrentStatus(), weaponManager.weaponLevel);
        DisplayCurrentStats();
    }

    public void OnBuildBaseScreen()
    {
        SetScreen(BaseLevelUpScreen);
        UpdateUpgradeInfo(baseManager.GetCurrentRequirements(), baseManager.baseLevel);
        DisplayCurrentStats();
    }

    public void OnTryBaseUpgrade()
    {
        BaseProgressionInfo currentStatus = new BaseProgressionInfo()
        {
            playerLevel = levelManager.currentLevel,
            iron = resourceTracker.GetResource("iron"),
            stone = resourceTracker.GetResource("stone"),
            boatCount = resourceTracker.GetResource("boat")
        };

        if (baseManager.CheckRequirements(currentStatus))
        {
            resourceTracker.SetResource("iron", -baseManager.currentRequirements.iron);
            resourceTracker.SetResource("stone", -baseManager.currentRequirements.stone);
            baseManager.UpgradeBase();
            UpdateUpgradeInfo(baseManager.GetCurrentRequirements(), baseManager.baseLevel);
            DisplayCurrentStats();
        }
    }

    public void OnTryPrimaryWeaponUpgrade()
    {
        WeaponProgressionInfo currentStatus = new WeaponProgressionInfo()
        {
            playerLevel = levelManager.currentLevel,
            iron = resourceTracker.GetResource("iron"),
            stone = resourceTracker.GetResource("stone"),
        };

        if (weaponManager.CheckRequirements(currentStatus))
        {
            resourceTracker.SetResource("iron", -weaponManager.currentProgressionState.iron);
            resourceTracker.SetResource("stone", -weaponManager.currentProgressionState.stone);
            weaponManager.UpgradeWeapon();
            UpdateUpgradeInfo(weaponManager.GetCurrentStatus(), weaponManager.weaponLevel);
            DisplayCurrentStats();
        }
    }

    public void OnTryCraftWeapon(CraftingAttemptPayload payload)
    {
        WeaponRecipeSO recipe = payload.weaponRecipe;

        WeaponCraftingRequirements currentStatus = new WeaponCraftingRequirements()
        {
            baseLevel = baseManager.baseLevel,
            ironCost = resourceTracker.GetResource("iron"),
            stoneCost = resourceTracker.GetResource("stone")
        };

        if (baseManager.CheckRequirements(recipe.craftingRequirements, currentStatus))
        {
            resourceTracker.SetResource("iron", -recipe.craftingRequirements.ironCost);
            resourceTracker.SetResource("stone", -recipe.craftingRequirements.stoneCost);
            weaponCraftedEvent.Raise(new WeaponCraftedPayload
            {
                weaponData = recipe.rewardWeapon
            });
            DisplayCurrentStats();
        }
    }

    public void ToggleBaseMenu(bool toggle)
    {
        isOpen = toggle;
        MenuCanvas.SetActive(toggle);
        OnMainMenu();

        PlayerManager.Instance.ToggleMovement(!toggle);
        PlayerManager.Instance.ToggleWeapons(!toggle);
        HudManager.Instance.ToggleHud(!toggle);
    }

    public void UpdateUpgradeInfo(BaseProgressionInfo currentRequirements, int level)
    {
        baseUpgradeUI.requirementsText.text = $"LV: {currentRequirements.playerLevel}\n" +
            $"Iron: {currentRequirements.iron}\n" +
            $"Stone: {currentRequirements.stone}\n";
        baseUpgradeUI.levelText.text = "LV: " + level;
    }

    public void UpdateUpgradeInfo(WeaponProgressionInfo currentRequirements, int level)
    {
        weaponUpgradeUI.requirementsText.text = $"LV: {currentRequirements.playerLevel}\n" +
            $"Iron: {currentRequirements.iron}\n" +
            $"Stone: {currentRequirements.stone}\n";
        weaponUpgradeUI.levelText.text = "LV: " + level;
    }

    private void SetScreen(GameObject activeScreen)
    {
        BaseLevelUpScreen.SetActive(false);
        WeaponCraftingScreen.SetActive(false);
        PrimaryWeaponUpgradeScreen.SetActive(false);
        MainBaseScreen.SetActive(false);

        activeScreen.SetActive(true);
    }

    private void DisplayCurrentStats()
    {
        statsTextBox.enabled = true;
        string stats = $"Current Player Level: {levelManager.currentLevel}\n" +
            $"Current Base Level: {baseManager.baseLevel}\n\n" +
            $"Iron: {resourceTracker.GetResource("iron")}\n" +
            $"Stone: {resourceTracker.GetResource("stone")}\n" +
            $"Boats: {resourceTracker.GetResource("boat")}\n";

        statsTextBox.text = stats;
    }

    private void HideCurrentStats()
    {
        statsTextBox.enabled = false;
    }
}

[Serializable]
public struct UpgradeInfoUI
{
    public TextMeshProUGUI requirementsText;
    public TextMeshProUGUI levelText;
}
