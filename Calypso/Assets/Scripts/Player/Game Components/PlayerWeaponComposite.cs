using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponComposite : MonoBehaviour
{

    [SerializeField] private List<WeaponController> secondaryWeapons;

    public void RefreshWeapons(List<WeaponController> newWeapons)
    {
        foreach (WeaponController weapon in secondaryWeapons)
        {
            weapon.DestroyWeaponInstance();
        }

        secondaryWeapons = newWeapons;

        foreach (WeaponController weapon in secondaryWeapons)
        {
            weapon.Initialize();
        }
    }
}
