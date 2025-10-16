using UnityEngine;

public class ResourcePickup : Pickup
{
    public override void CollectPickup(PickupSO data)
    {
        switch (data.resourceType)
        {
            case ResourceType.iron:
                ResourceTracker.Instance.AddIron(data.pickupValue);
                break;
            case ResourceType.exp:
                LevelManager.Instance.AddExperience(data.pickupValue);
                break;
            default:
                Debug.LogWarning("Unhandled resource type: " + data.resourceType);
                break;
        }

        Destroy(this.gameObject);
    }
}
