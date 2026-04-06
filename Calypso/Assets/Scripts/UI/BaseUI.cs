using System;
using TMPro;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private OnCraftingAttemptEventSO craftingAttemptEvent;
    [SerializeField] private OnWeaponCraftedEventSO weaponCraftedEvent;
    [SerializeField] private OnDeathEventSO playerDeathEvent;

    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject BaseLevelUpScreen;
    [SerializeField] private GameObject WeaponCraftingScreen;
    [SerializeField] private GameObject PrimaryWeaponUpgradeScreen;
    [SerializeField] private GameObject MainBaseScreen;
    [SerializeField] private TextMeshProUGUI baseTextBox;
    [SerializeField] private TextMeshProUGUI weaponTextBox;

    [SerializeField] private UpgradeInfoUI baseUpgradeUI;
    [SerializeField] private UpgradeInfoUI weaponUpgradeUI;

    private BaseManager baseManager;
    private PlayerPrimaryWeaponManager weaponManager;
    private ResourceTracker resourceTracker;
    private PlayerLevelManager levelManager;

    public static BaseUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    [HideInInspector]
    public bool isOpen = false;

    private void OnEnable()
    {
        craftingAttemptEvent.RegisterListener(OnTryCraftWeapon);
        playerDeathEvent.RegisterListener(ExitBase);
    }

    private void OnDisable()
    {
        craftingAttemptEvent.UnregisterListener(OnTryCraftWeapon);
        playerDeathEvent.UnregisterListener(ExitBase);
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
        BlueprintManager.Instance.CheckRecipes();
        HideCurrentStats();
    }

    public void OnPrimaryWeaponScreen()
    {
        SetScreen(PrimaryWeaponUpgradeScreen);
        UpdateUpgradeInfo(weaponManager.GetCurrentStatus(), weaponManager.weaponLevel);
        DisplayWeaponStats();
    }

    public void OnBuildBaseScreen()
    {
        SetScreen(BaseLevelUpScreen);
        UpdateUpgradeInfo(baseManager.GetCurrentRequirements(), baseManager.baseLevel);
        DisplayBaseStats();
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
            DisplayBaseStats();
        }
    }

    public void OnTryPrimaryWeaponUpgrade()
    {
        WeaponProgressionInfo currentStatus = new WeaponProgressionInfo()
        {
            BaseLevel = baseManager.baseLevel,
            iron = resourceTracker.GetResource("iron"),
            stone = resourceTracker.GetResource("stone"),
        };

        if (weaponManager.CheckRequirements(currentStatus))
        {
            resourceTracker.SetResource("iron", -weaponManager.currentProgressionState.iron);
            resourceTracker.SetResource("stone", -weaponManager.currentProgressionState.stone);
            weaponManager.UpgradeWeapon();
            UpdateUpgradeInfo(weaponManager.GetCurrentStatus(), weaponManager.weaponLevel);
            DisplayWeaponStats();
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
            weaponCraftedEvent.Raise(new WeaponCraftedPayload { weaponData = recipe.rewardWeapon });
        }
    }

    private void ExitBase(DeathPayload payload) => ToggleBaseMenu(false);

    public void ToggleBaseMenu(bool toggle)
    {
        isOpen = toggle;
        MenuCanvas.SetActive(toggle);
        OnMainMenu();
        Time.timeScale = toggle ? 0 : 1;

        PlayerManager.Instance.ToggleMovement(!toggle);
        PlayerManager.Instance.ToggleWeapons(!toggle);
        PlayerManager.Instance.ToggleMenuControls(!toggle);
        HudManager.Instance.ToggleHud(!toggle);
    }

    public void UpdateUpgradeInfo(BaseProgressionInfo currentRequirements, int level)
    {
        baseUpgradeUI.requirementsText.text = $"Player Lv: {currentRequirements.playerLevel}\n" +
            $"Iron: {currentRequirements.iron}\n" +
            $"Stone: {currentRequirements.stone}\n";
        baseUpgradeUI.levelText.text = "LV: " + level;
    }

    public void UpdateUpgradeInfo(WeaponProgressionInfo currentRequirements, int level)
    {
        weaponUpgradeUI.requirementsText.text = $"Base Lv: {currentRequirements.BaseLevel}\n" +
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

    private void DisplayBaseStats()
    {
        baseTextBox.enabled = true;
        string stats = $"Player Lv: {levelManager.currentLevel}\n" +
            $"Iron: {resourceTracker.GetResource("iron")}\n" +
            $"Stone: {resourceTracker.GetResource("stone")}\n";

        baseTextBox.text = stats;
    }

    private void DisplayWeaponStats()
    {
        weaponTextBox.enabled = true;
        string stats = $"Base Lv: {baseManager.baseLevel}\n" +
            $"Iron: {resourceTracker.GetResource("iron")}\n" +
            $"Stone: {resourceTracker.GetResource("stone")}\n";

        weaponTextBox.text = stats;
    }

    private void HideCurrentStats()
    {
        baseTextBox.enabled = false;
        weaponTextBox.enabled = false;
    }
}

[Serializable]
public struct UpgradeInfoUI
{
    public TextMeshProUGUI requirementsText;
    public TextMeshProUGUI levelText;
}
