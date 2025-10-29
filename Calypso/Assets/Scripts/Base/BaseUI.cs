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

    public void OnBaseLevelUpButton()
    {
        SetScreen(BaseLevelUpScreen);
    }

    public void OnWeaponCraftingButton()
    {
        SetScreen(WeaponCraftingScreen);
    }

    public void OnPrimaryWeaponButton()
    {
        SetScreen(PrimaryWeaponUpgradeScreen);
    }

    public void OnReturnToMainMenu()
    {
        SetScreen(MainBaseScreen);
    }

    public void ToggleBaseMenu(bool toggle)
    {
        isOpen = toggle;
        MenuCanvas.SetActive(toggle);
        SetScreen(MainBaseScreen);

        PlayerManager.Instance.ToggleMovement(!toggle);
        PlayerManager.Instance.ToggleWeapons(!toggle);
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
