using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    [SerializeField]
    private Pickup pickup;

    private void Awake()
    {
        if (pickup == null)
            pickup = GetComponentInParent<Pickup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            pickup.CollectPickup(pickup.GetPickupData());
    }
}
