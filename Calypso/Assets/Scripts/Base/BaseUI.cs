using System;
using TMPro;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject BaseLevelUpScreen;
    [SerializeField] private GameObject WeaponCraftingScreen;
    [SerializeField] private GameObject PrimaryWeaponUpgradeScreen;
    [SerializeField] private GameObject MainBaseScreen;

    [SerializeField] private BaseManager baseManager;
    [SerializeField] private PlayerPrimaryWeaponManager weaponManager;
    [SerializeField] private ResourceTracker resourceTracker;
    [SerializeField] private PlayerLevelManager levelManager;

    [SerializeField] private UpgradeInfoUI baseUpgradeUI;
    [SerializeField] private UpgradeInfoUI weaponUpgradeUI;

    [HideInInspector]
    public bool isOpen = false;

    public void OnMainMenu()
    {
        SetScreen(MainBaseScreen);
    }

    public void OnCraftingScreen()
    {
        SetScreen(WeaponCraftingScreen);
    }

    public void OnPrimaryWeaponScreen()
    {
        SetScreen(PrimaryWeaponUpgradeScreen);
        UpdateUpgradeInfo(weaponManager.GetCurrentStatus(), weaponManager.weaponLevel);
    }

    public void OnBuildBaseScreen()
    {
        SetScreen(BaseLevelUpScreen);
        UpdateUpgradeInfo(baseManager.GetCurrentRequirements(), baseManager.baseLevel);
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
        }

    }
    
    public void ToggleBaseMenu(bool toggle)
    {
        isOpen = toggle;
        MenuCanvas.SetActive(toggle);
        SetScreen(MainBaseScreen);

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
}

[Serializable]
public struct UpgradeInfoUI
{
    public TextMeshProUGUI requirementsText;
    public TextMeshProUGUI levelText;
}
