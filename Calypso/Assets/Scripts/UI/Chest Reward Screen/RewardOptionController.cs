using UnityEngine;

public class RewardOptionController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private OptionUIComponents components;

    [Header("Resources")]
    [SerializeField] private Sprite emptySprite;

    public RewardOption GetItem()
    {
        return components.assignedItem;
    }

    public void ToggleInfoPanel(bool toggle)
    {
        components.InfoPanel.infoPanelParent.SetActive(toggle);
    }

    public void UpdateOptionUI(RewardOption option)
    {
        string upgradeInfo = "";

        if (option.isNew)
        {
            upgradeInfo = GetBaseStatInfo(option);
        }
        else
        {
            if (option.deltaValues != null)
                upgradeInfo = GetUpgradeInfo(option);
        }

        components.assignedItem = option;
        components.isNewText.SetActive(option.isNew);
        components.iconImage.sprite = option.itemData.sprite;
        components.InfoPanel.itemName.text = option.itemData.itemName;
        components.InfoPanel.itemDescription.text = option.itemData.description;
        components.InfoPanel.statInfo.text = upgradeInfo;
        components.group.interactable = true;
        components.group.blocksRaycasts = true;
    }

    public void DisableOption()
    {
        components.assignedItem = null;
        components.isNewText.SetActive(false);
        components.iconImage.sprite = emptySprite;
        components.InfoPanel.itemName.text = "";
        components.InfoPanel.itemDescription.text = "";
        components.InfoPanel.statInfo.text = "";
        components.group.interactable = false;
        components.group.blocksRaycasts = false;

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
