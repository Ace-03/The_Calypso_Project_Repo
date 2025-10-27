using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private OnChestOpenedEventSO chestOpenedEvent;

    public void OnOpened()
    {
        var payload = new RewardPayload();
        {
            // Initialize payload properties likely through a RewardGenerator
        }

        chestOpenedEvent.Raise(payload);

        Destroy(gameObject);
    }
}
