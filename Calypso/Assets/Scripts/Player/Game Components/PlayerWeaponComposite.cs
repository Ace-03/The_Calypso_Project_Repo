using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponComposite : MonoBehaviour
{
    [SerializeField] private OnWeaponsUpdatedEventSO weaponsUpdatedEvent;

    private List<WeaponController> weaponInstances = new List<WeaponController>();
    private List<WeaponDefinitionSO> weaponDefinitions = new List<WeaponDefinitionSO>();

    private void OnEnable()
    {
        weaponsUpdatedEvent.RegisterListener(RefreshWeapons);
    }

    private void OnDisable()
    {
        weaponsUpdatedEvent.UnregisterListener(RefreshWeapons);
    }

    private void Start()
    {
        if (weaponInstances == null || weaponInstances.Count <= 0) { return; }

        RefreshWeapons(new WeaponsUpdatePayload() 
        { 
            weapons = weaponInstances.Select(weapon => weapon.GetWeaponData()).ToList() 
        });
    }

    public void RefreshWeapons(WeaponsUpdatePayload payload)
    {
        weaponDefinitions = payload.weapons;

        if (weaponInstances != null || weaponInstances.Count > 0)
        {
            foreach (WeaponController weaponInstance in weaponInstances)
            {
                weaponInstance.DestroyWeaponInstance();
                Destroy(weaponInstance);
            }
        }

        for (int i = 0; i < weaponDefinitions.Count; i++)
        {
            WeaponDefinitionSO weaponData = weaponDefinitions[i];
            WeaponController weaponController = gameObject.AddComponent<WeaponController>();
            weaponController.SetWeaponData(weaponData);
            weaponInstances.Add(weaponController);
        }

        foreach (WeaponController weapon in weaponInstances)
        {
            weapon.Initialize();
        }
    }
}
