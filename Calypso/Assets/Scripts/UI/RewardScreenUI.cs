using UnityEngine;

public class RewardScreenUI : MonoBehaviour
{
    [SerializeField] private OnRewardsGeneratedSO rewardsGeneratedEvent;
    [SerializeField] private OnRewardSelectedEventSO rewardsSelectedEvent;
    [SerializeField] private RewardScreenUIComponents components;

    private void OnEnable()
    {
        rewardsGeneratedEvent.RegisterListener(DisplayRewardScreen);
    }

    private void OnDisable()
    {
        rewardsGeneratedEvent.UnregisterListener(DisplayRewardScreen);
    }

    private void DisplayRewardScreen(RewardOptionsPayload payload)
    {
        Time.timeScale = 0;
        components.RewardScreenParent.SetActive(true);

        if (payload.options.Count < components.rewardOptions.Count)
        {
            for (int i = payload.options.Count; i < components.rewardOptions.Count; i++)
            {
                components.rewardOptions[i].DisableOption();
            }
        }

        for (int i = 0; i < payload.options.Count; i++)
        {
            components.rewardOptions[i].UpdateOptionUI(payload.options[i]);
        }
    }

    public void OnRewardSelected(int index)
    {
        SelectedRewardPayload payload = new SelectedRewardPayload()
        {
            option = components.rewardOptions[index].GetItem(),
        };

        rewardsSelectedEvent.Raise(payload);
        HideRewardsScreen();
    }
    public void OnDisplayInfoPanel(int index)
    {
        components.rewardOptions[index].ToggleInfoPanel(true);
    }

    public void OnHideInfoPanel(int index)
    {
        components.rewardOptions[index].ToggleInfoPanel(false);
    }

    private void HideRewardsScreen()
    {
        Time.timeScale = 1;
        components.RewardScreenParent.SetActive(false);
        OnHideInfoPanel(0);
        OnHideInfoPanel(1);
        OnHideInfoPanel(2);

        for (int i = 0; i < components.rewardOptions.Count; i++)
        {
            OnHideInfoPanel(i);
        }
    }
}
