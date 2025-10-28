using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryWeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponController playerWeaponController;
    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private List<WeaponDefinitionSO> weaponStages;

    public void UpgradeWeapon()
    {
        Mathf.Clamp(weaponLevel, 1, weaponStages.Count + 1);
        weaponLevel++;

        playerWeaponController.SetWeaponData(GetCurrentWeapon());
    }

    public WeaponDefinitionSO GetCurrentWeapon()
    {
        return weaponStages[weaponLevel - 1];
    }
}
