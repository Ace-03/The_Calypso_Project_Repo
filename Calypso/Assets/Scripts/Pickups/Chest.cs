using UnityEngine;

public class Chest : Pickup
{
    [SerializeField]
    private OnRewardRequestedEventSO chestOpenedEvent;

    private void Start()
    {
        StopAnimation();
    }

    public override void CollectPickup(PickupSO data)
    {
        OnOpened();
    }

    public void OnOpened()
    {
        SelectedRewardPayload payload = new SelectedRewardPayload();
        {
            // no data needed for now
        }

        chestOpenedEvent.Raise(payload);

        Destroy(gameObject);
    }
}
