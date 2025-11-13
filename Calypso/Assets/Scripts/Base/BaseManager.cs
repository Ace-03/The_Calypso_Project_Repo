using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public int baseLevel;
    [SerializeField] private BaseProgressionSO progressionData;

    public BaseProgressionInfo currentRequirements;

    private void Start()
    {
        currentRequirements = progressionData.BaseLevelProgression[baseLevel];
        baseLevel = 0;
    }

    public void UpgradeBase()
    {
        baseLevel++;
        currentRequirements = progressionData.BaseLevelProgression[baseLevel];
    }

    public bool CheckRequirements(BaseProgressionInfo currentStatus)
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

    public bool CheckRequirements(WeaponCraftingRequirements requirements,  WeaponCraftingRequirements currentStats)
    {
        bool passes = true;
        string logMessage = "Crafting Status: ";

        if (currentStats.baseLevel < requirements.baseLevel)
        {
            logMessage += "Base Level Is too low, ";
            passes = false;
        }
        if (currentStats.ironCost < requirements.ironCost)
        {
            logMessage += "Not Enough Iron, ";
            passes = false;
        }
        if (currentStats.stoneCost < requirements.stoneCost)
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
            Debug.LogWarning(logMessage + ": Failed to Craft Weapon");
        }

        return passes;
    }

    public BaseProgressionInfo GetCurrentRequirements()
    {
        return currentRequirements;
    }
}
