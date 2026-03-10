using UnityEngine;

public class PlayerPrimaryWeaponManager : MonoBehaviour
{
    public static PlayerPrimaryWeaponManager Instance;

    public int weaponLevel = 1;
    [SerializeField] private WeaponController playerWeaponController;
    [SerializeField] private PrimaryWeaponProgressionSO progressionData;
    [SerializeField] private OnTutorialTriggerEventSO tutorialTrigger;
    [SerializeField] private OnUpgradeAttemptEventSO upgradeAttemptEvent;
    public WeaponProgressionInfo currentProgressionState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        weaponLevel = 1;
        currentProgressionState = progressionData.weaponLevelProgression[0];
    }

    public void UpgradeWeapon()
    {
        playerWeaponController.SetWeaponData(GetRewardWeapon());
        playerWeaponController.SetDamageSource(new DamageSource(GetRewardWeapon(), gameObject));
        currentProgressionState = progressionData.weaponLevelProgression[Mathf.Clamp(weaponLevel, 0, progressionData.weaponLevelProgression.Count - 1)];
        weaponLevel++;

        if (weaponLevel == 2)
            tutorialTrigger.Raise(new TutorialTriggerPayload { tutorialNumber = 2 });
    }

    public bool CheckRequirements(WeaponProgressionInfo currentStatus)
    {
        bool passes = true;
        string logMessage = "";

        if (currentStatus.BaseLevel < currentProgressionState.BaseLevel)
        {
            logMessage += "Base Level Is too low\n";
            passes = false;
        }
        if (currentStatus.iron < currentProgressionState.iron)
        {
            logMessage += "Not Enough Iron\n";
            passes = false;
        }
        if (currentStatus.stone < currentProgressionState.stone)
        {
            logMessage += "Not Enough Stone\n";
            passes = false;
        }

        if (passes)
        {
            Debug.Log(logMessage + "Weapon Successfully Upgraded");
        }

        upgradeAttemptEvent.Raise(new UpgradeAttemptPayload
        {
            Result = passes ? "Weapon Successfully Upgraded" : "Cannot Upgrade Weapon",
            details = logMessage,
        });

        return passes;
    }

    public WeaponProgressionInfo GetCurrentStatus()
    {
        return currentProgressionState;
    }

    public WeaponDefinitionSO GetRewardWeapon()
    {
        return currentProgressionState.LevelUpReward;
    }
}
