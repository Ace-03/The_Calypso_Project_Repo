using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField] private OnRestoreHealthEventSO healEvent;
    public override void CollectPickup(PickupSO data)
    {
        if (data.pickupType == "health")
        {
            healEvent.Raise(new HealPayload { value = data.pickupValue });
        }

        Destroy(gameObject);
    }
}
