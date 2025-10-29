using UnityEngine;

public class ResourcePickup : Pickup
{
    public override void CollectPickup(PickupSO data)
    {
        if (data.pickupType == "exp")
        {
            LevelManager.Instance.AddExperience(data.pickupValue);
        }
        else 
        {
            ResourceTracker.Instance.SetResource(data.pickupType, data.pickupValue);
        }

        Destroy(this.gameObject);
    }
}
