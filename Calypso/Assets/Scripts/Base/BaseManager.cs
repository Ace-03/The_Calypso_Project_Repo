using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private int BaseLevel;
    [SerializeField] private BaseProgressionSO progressionData;
    [SerializeField] private PlayerPrimaryWeaponManager primaryWeaponManager;

    private BaseLevelUpRequirements currentRequirements;

    private void Start()
    {
        currentRequirements = progressionData.BaseLevelProgression[BaseLevel];
        BaseLevel = 0;
    }

    public void UpgradeBase()
    {
        BaseLevel++;
        currentRequirements = progressionData.BaseLevelProgression[BaseLevel];
    }

    public bool CheckRequirements(BaseLevelUpRequirements currentStatus)
    {
        bool passes = true;
        string logMessage = "Base Upgrade Status: ";

        if (currentStatus.playerLevel < currentRequirements.playerLevel)
        {
            logMessage += "Player Level Is too low, ";
            passes = false;
        }
        if (currentStatus.iron < currentRequirements.iron)
        {
            logMessage += "Not Enough Iron, ";
            passes = false;
        }
        if (currentStatus.stone < currentRequirements.stone)
        {
            logMessage += "Not Enough Stone, ";
            passes = false;
        }
        if (currentStatus.boatCount < currentRequirements.boatCount)
        {
            logMessage += "Lacking in boats";
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

    public void UpgradePrimaryWeapon()
    {
        primaryWeaponManager.UpgradeWeapon();
    }
}
