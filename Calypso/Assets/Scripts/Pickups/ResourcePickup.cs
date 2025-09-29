using UnityEngine;

public class ResourcePickup : Pickup
{
    public override void CollectPickup(PickupSO data)
    {
        switch (data.resourceType)
        {
            case ResourceType.Resin:
                ResourceTracker.Instance.AddResin(data.pickupValue);
                break;
            case ResourceType.Stone:
                ResourceTracker.Instance.AddStone(data.pickupValue);
                break;
            default:
                Debug.LogWarning("Unhandled resource type: " + data.resourceType);
                break;
        }

    }
}
