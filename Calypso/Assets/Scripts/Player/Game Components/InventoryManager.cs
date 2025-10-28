using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private OnRewardSelectedEventSO rewardSelectedEvent;
    [SerializeField] private int maxPassiveItems;
    [SerializeField] private int maxWeapons;

    private void OnEnable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);
    }

    private void OnDisable()
    {
        rewardSelectedEvent.RegisterListener(ProcessSelectedReward);

    }

    private void ProcessSelectedReward(RewardOption option)
    {

    }
}
