using UnityEngine;

public class ResourcePickup : Pickup
{
    public override void CollectPickup(PickupSO data)
    {
        switch (data.resourceType)
        {
            case ResourceType.iron:
                ResourceTracker.Instance.SetIron(data.pickupValue);
                break;
            case ResourceType.exp:
                PlayerLevelManager.Instance.AddExperience(data.pickupValue);
                break;
            default:
                Debug.LogWarning("Unhandled resource type: " + data.resourceType);
                break;
        }

        Destroy(this.gameObject);
    }
}
