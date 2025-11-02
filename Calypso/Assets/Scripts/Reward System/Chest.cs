using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private OnRewardRequestedEventSO chestOpenedEvent;

    public void OnOpened()
    {
        var payload = new SelectedRewardPayload();
        {
            // Initialize payload properties likely through a RewardGenerator
        }

        chestOpenedEvent.Raise(payload);

        Destroy(gameObject);
    }
}
