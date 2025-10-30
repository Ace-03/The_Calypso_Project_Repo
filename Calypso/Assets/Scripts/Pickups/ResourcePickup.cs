using UnityEngine;

public class ResourcePickup : Pickup
{
    public override void CollectPickup(PickupSO data)
    {
        if (data.pickupType == "exp")
        {
            PlayerLevelManager.Instance.AddExperience(data.pickupValue);
        }
        else 
        {
            ResourceTracker.Instance.SetResource(data.pickupType, data.pickupValue);
        }

        Destroy(this.gameObject);
    }
}
