using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryWeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponController playerWeaponController;
    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private PrimaryWeaponProgressionSO progressionData;

    private WeaponProgressionInfo currentProgressionState;

    private void Start()
    {
        weaponLevel = 1;
        currentProgressionState = progressionData.weaponLevelProgression[0];
    }

    public void UpgradeWeapon()
    {
        currentProgressionState = progressionData.weaponLevelProgression[Mathf.Clamp(weaponLevel, 0, progressionData.weaponLevelProgression.Count - 1)];
        weaponLevel++;

        playerWeaponController.SetWeaponData(GetCurrentWeapon());
    }

    public bool CheckRequirements(BaseLevelUpRequirements currentStatus)
    {
        bool passes = true;
        string logMessage = "Weapon Upgrade Status: ";

        if (currentStatus.playerLevel < currentProgressionState.playerLevel)
        {
            logMessage += "Player Level Is too low, ";
            passes = false;
        }
        if (currentStatus.iron < currentProgressionState.iron)
        {
            logMessage += "Not Enough Iron, ";
            passes = false;
        }
        if (currentStatus.stone < currentProgressionState.stone)
        {
            logMessage += "Not Enough Stone, ";
            passes = false;
        }

        if (passes)
        {
            Debug.Log(logMessage + "Success");
        }
        else
        {
            Debug.LogWarning(logMessage + ": Failed to level up");
        }

        return passes;
    }

    public WeaponDefinitionSO GetCurrentWeapon()
    {
        return currentProgressionState.weapon;
    }
}
