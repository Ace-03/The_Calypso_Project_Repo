using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private OnGenericEventSO passiveSlotUpgrade;
    [SerializeField] private OnGenericEventSO weaponSlotUpgrade;
    [SerializeField] private OnTutorialTriggerEventSO tutorialTrigger;
    [SerializeField] private OnUpgradeAttemptEventSO upgradeAttemptEvent;

    public int baseLevel;
    [SerializeField] private BaseProgressionSO progressionData;

    [SerializeField] private GameObject militaryModel;
    [SerializeField] private GameObject fishingModel;
    [SerializeField] private GameObject sailModel;

    public BaseProgressionInfo currentRequirements;

    public static BaseManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        currentRequirements = progressionData.BaseLevelProgression[baseLevel];
        baseLevel = 0;
    }

    public void UpgradeBase()
    {
        baseLevel++;
        currentRequirements = progressionData.BaseLevelProgression[baseLevel];

        if (ResourceTracker.Instance.hasSailBoat && !sailModel.activeSelf)
        {
            sailModel.SetActive(true);
        }
        else if (ResourceTracker.Instance.hasFishingBoat && !fishingModel.activeSelf)
        {
            fishingModel.SetActive(true);
            weaponSlotUpgrade.Raise(new GameEventPayload());
        }
        else if (ResourceTracker.Instance.hasMilitaryBoat && !militaryModel.activeSelf)
        {
            militaryModel.SetActive(true);
            passiveSlotUpgrade.Raise(new GameEventPayload());
        }

        if (baseLevel == 1)
            tutorialTrigger.Raise(new TutorialTriggerPayload { tutorialNumber = 1 });
    }

    public bool CheckRequirements(BaseProgressionInfo currentStatus)
    {
        bool passes = true;
        string logMessage = "";

        if (currentStatus.playerLevel < currentRequirements.playerLevel)
        {
            logMessage += "Player Level Is too low\n";
            passes = false;
        }
        if (currentStatus.iron < currentRequirements.iron)
        {
            logMessage += "Not Enough Iron\n";
            passes = false;
        }
        if (currentStatus.stone < currentRequirements.stone)
        {
            logMessage += "Not Enough Stone\n";
            passes = false;
        }
        if (currentStatus.boatCount < currentRequirements.boatCount)
        {
            logMessage += "No boat to craft base with";
            passes = false;
        }

        if (passes)
        {
            Debug.Log(logMessage + "Base Succesffully Upgraded");
        }

        upgradeAttemptEvent.Raise(new UpgradeAttemptPayload
        {
            Result = passes ? "Base Succesffully Upgraded" : "Cannot Upgrade Base",
            details = logMessage,
        });

        return passes;
    }

    public bool CheckRequirements(WeaponCraftingRequirements requirements,  WeaponCraftingRequirements currentStats)
    {
        bool passes = true;
        string logMessage = "";

        if (currentStats.baseLevel < requirements.baseLevel)
        {
            logMessage += "Base Level Is too low\n";
            passes = false;
        }
        if (currentStats.ironCost < requirements.ironCost)
        {
            logMessage += "Not Enough Iron\n";
            passes = false;
        }
        if (currentStats.stoneCost < requirements.stoneCost)
        {
            logMessage += "Not Enough Stone\n";
            passes = false;
        }

        if (passes)
        {
            Debug.Log(logMessage + "Weapon Successfully Crafted");
        }

        upgradeAttemptEvent.Raise(new UpgradeAttemptPayload
        {
            Result = passes ? "Weapon Successfully Crafted" : "Cannot Craft Weapon",
            details = logMessage,
        });

        return passes;
    }

    public BaseProgressionInfo GetCurrentRequirements()
    {
        return currentRequirements;
    }
}
