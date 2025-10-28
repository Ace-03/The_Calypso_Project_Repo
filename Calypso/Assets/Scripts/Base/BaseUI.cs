using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject BaseLevelUpScreen;
    [SerializeField] private GameObject WeaponCraftingScreen;
    [SerializeField] private GameObject PrimaryWeaponUpgradesScreen;
    [SerializeField] private GameObject MainBaseScreen;

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
        SetScreen(PrimaryWeaponUpgradesScreen);
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
        PrimaryWeaponUpgradesScreen.SetActive(false);
        MainBaseScreen.SetActive(false);

        activeScreen.SetActive(true);
    }

}
