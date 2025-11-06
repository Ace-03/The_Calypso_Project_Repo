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

        for (int i = 0; i < payload.options.Count; i++)
        {
            UpdateOptionUI(i, payload.options[i]);
        }
    }

    private void UpdateOptionUI(int index, RewardOption option)
    {
        RewardOptionUIComponents rewardOptionUI = components.rewardOptions[index];
        RewardInfoPanelComponents rewardInfoPanelUI = rewardOptionUI.rewardInfoPanel;
        string upgradeInfo = "";

        if (option.isNew)
        {
            upgradeInfo = GetBaseStatInfo(option);
        }
        else
        {
            upgradeInfo = GetUpgradeInfo(option);
        }

        rewardOptionUI.assignedItem = option;
        rewardOptionUI.isNewText.SetActive(option.isNew);
        rewardOptionUI.iconImage.sprite = option.itemData.sprite;
        rewardInfoPanelUI.itemName.text = option.itemData.itemName;
        rewardInfoPanelUI.itemDescription.text = option.itemData.description;
        rewardInfoPanelUI.statInfo.text = upgradeInfo;

        components.rewardOptions[index] = rewardOptionUI;
    }

    public void OnRewardSelected(int index)
    {
        Debug.Log($"index sekected is: {index}");
        SelectedRewardPayload payload = new SelectedRewardPayload()
        {
            option = components.rewardOptions[index].assignedItem,
        };

        rewardsSelectedEvent.Raise(payload);
        HideRewardsScreen();
    }
    public void OnDisplayInfoPanel(int index)
    {
        Debug.Log("Display Info Panel");
        components.rewardOptions[index].rewardInfoPanel.infoPanelParent.SetActive(true);
    }

    public void OnHideInfoPanel(int index)
    {
        Debug.Log("Hide Info Panel");
        components.rewardOptions[index].rewardInfoPanel.infoPanelParent.SetActive(false);

    }

    private void HideRewardsScreen()
    {
        Time.timeScale = 1;
        components.RewardScreenParent.SetActive(false);
    }

    private string GetBaseStatInfo(RewardOption option)
    {
        string upgradeInfo = "";

        foreach (StatModifier stat in option.modifiers)
        {
            string applicationType = "+";
            string unitType = "";

            if (stat.ModType == StatModifierType.MultPercentage ||
                stat.ModType == StatModifierType.AdditivePercentage)
            {
                unitType = "%";

                if (stat.ModType == StatModifierType.MultPercentage)
                {
                    applicationType = "X";
                }
            }

            string value = $"{applicationType}{stat.Value.ToString("F1")}{unitType}";

            upgradeInfo += $"{stat.StatType}: {value}\n";
        }

        return upgradeInfo;
    }

    private string GetUpgradeInfo(RewardOption option)
    {
        string upgradeInfo = "";

        foreach (StatChangeDelta statChange in option.deltaValues)
        {
            string applicationType = "+";
            string unitType = "";

            if (statChange.ModType == StatModifierType.MultPercentage ||
                statChange.ModType == StatModifierType.AdditivePercentage)
            {
                unitType = "%";

                if (statChange.ModType == StatModifierType.MultPercentage)
                {
                    applicationType = "X";
                }
            }

            string oldValue = $"{applicationType}{statChange.oldValue.ToString("F1")}{unitType}";
            string newValue = $"{applicationType}{statChange.newValue.ToString("F1")}{unitType}";

            upgradeInfo += $"{statChange.Type}: {oldValue} --> {newValue}\n";
        }

        return upgradeInfo;
    }
}
