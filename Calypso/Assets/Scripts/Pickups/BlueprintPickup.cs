using UnityEngine;

public class BlueprintPickup : Pickup
{
    [SerializeField] private OnBlueprintCollectedEventSO blueprintCollectedEvent;

    public override void CollectPickup(PickupSO data)
    {
        if (data.pickupType != "blueprint")
        {
            Debug.LogError("Pickup Is Not Of Type Boat");
        }

        blueprintCollectedEvent.Raise(new BlueprintCollectedPayload {
            weaponName = data.weaponToUnlock.weaponName,
            icon = data.weaponToUnlock.blueprintIcon,
        });

        Destroy(gameObject);
    }
}
