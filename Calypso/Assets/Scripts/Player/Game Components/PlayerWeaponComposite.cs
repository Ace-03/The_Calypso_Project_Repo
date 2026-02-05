using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerWeaponComposite : MonoBehaviour
{
    [SerializeField] private OnWeaponsUpdatedEventSO weaponsUpdatedEvent;

    private List<WeaponController> weaponInstances = new List<WeaponController>();
    private List<WeaponDefinitionSO> weaponDefinitions = new List<WeaponDefinitionSO>();
    private bool weaponsActive;

    private void OnEnable()
    {
        weaponsUpdatedEvent.RegisterListener(RefreshWeapons);
        Debug.Log("weapon composite IS listening for events");
    }

    private void OnDisable()
    {
        weaponsUpdatedEvent.UnregisterListener(RefreshWeapons);
        Debug.Log("weapon composite NOT listening for events");
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
        Debug.LogWarning($"In Refresh weapons");
        weaponDefinitions = payload.weapons;

        foreach (WeaponDefinitionSO weapon in weaponDefinitions)
            Debug.LogWarning($"weapon composite has {weapon.name}");

        if (weaponInstances != null || weaponInstances.Count > 0)
        {
            foreach (WeaponController weaponInstance in weaponInstances)
            {
                weaponInstance.DestroyWeaponInstance();
            }
        }

        for (int i = 0; i < weaponDefinitions.Count; i++)
        {
            WeaponDefinitionSO weaponData = weaponDefinitions[i];
            WeaponController weaponCtrl = gameObject.AddComponent<WeaponController>();
            weaponCtrl.SetWeaponData(weaponData);
            weaponCtrl.SetDamageSource(new DamageSource(weaponData, gameObject));
            weaponCtrl.Initialize();

            weaponInstances.Add(weaponCtrl);

            if (!weaponsActive)
            {
                weaponCtrl.enabled = false;
                //weaponCtrl.weaponPivot.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleWeapons(bool toggle)
    {
        weaponsActive = toggle;
        foreach (WeaponController weapon in weaponInstances)
        {
            weapon.enabled = toggle;
            //weapon.weaponPivot.gameObject.SetActive(toggle);
        }
    }
}
