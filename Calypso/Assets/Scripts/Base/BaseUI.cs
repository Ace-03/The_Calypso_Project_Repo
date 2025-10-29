using System;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject BaseLevelUpScreen;
    [SerializeField] private GameObject WeaponCraftingScreen;
    [SerializeField] private GameObject PrimaryWeaponUpgradeScreen;
    [SerializeField] private GameObject MainBaseScreen;

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
    }

    public void OnBuildBaseScreen()
    {
        SetScreen(BaseLevelUpScreen);
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

    private void SetScreen(GameObject activeScreen)
    {
        BaseLevelUpScreen.SetActive(false);
        WeaponCraftingScreen.SetActive(false);
        PrimaryWeaponUpgradeScreen.SetActive(false);
        MainBaseScreen.SetActive(false);

        activeScreen.SetActive(true);
    }
}
