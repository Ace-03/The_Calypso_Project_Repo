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

        weaponInstances.Clear();

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
