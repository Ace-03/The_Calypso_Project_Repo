using UnityEngine;
public class BoatPickup : Pickup
{
    public override void CollectPickup(PickupSO data)
    {
        if (data.pickupType != "boat")
        {
            Debug.LogError("Pickup Is Not Of Type Boat");
        }

        ResourceTracker.Instance.SetResource("boat", data.pickupValue);

        Debug.Log("BoatCount: " + ResourceTracker.Instance.GetResource("boat"));

        if (data.pickupName == "Military Boat")
            ResourceTracker.Instance.hasMilitaryBoat = true;
        else if (data.pickupName == "Sail Boat")
            ResourceTracker.Instance.hasSailBoat = true;
        else if (data.pickupName == "Fishing Boat")
            ResourceTracker.Instance.hasFishingBoat = true;

        HudManager.Instance.resources.UpdateBoatIcons(data.sprite);

        Destroy(gameObject);
    }
}
