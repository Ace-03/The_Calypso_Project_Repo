using Unity.VisualScripting;
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

        HudManager.Instance.resources.UpdateBoatIcons(data.sprite);

        Destroy(this.gameObject);
    }
}
