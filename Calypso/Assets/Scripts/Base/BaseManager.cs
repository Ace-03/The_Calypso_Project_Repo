using UnityEngine;

public class BaseManager : MonoBehaviour
{
    [SerializeField] private int BaseLevel;
    [SerializeField] private BaseProgressionSO progressionData;
    [SerializeField] private PlayerPrimaryWeaponManager primaryWeaponManager;

    private int PlayerLevelRequirement;

    private void Start()
    {
        BaseLevel = 0;
    }

    public void UpgradeBase()
    {
        PlayerLevelRequirement = BaseLevel < progressionData.LevelRequirements.Count ?
            progressionData.LevelRequirements[BaseLevel] : PlayerLevelRequirement + 5;
        BaseLevel++;
    }

    public void UpgradePrimaryWeapon()
    {
        primaryWeaponManager.UpgradeWeapon();
    }
}
