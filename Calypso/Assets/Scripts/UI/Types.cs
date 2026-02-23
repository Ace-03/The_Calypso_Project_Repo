using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct RewardScreenUIComponents
{
    public GameObject RewardScreenParent;
    public GameObject RewardsContainer;
    public List<OptionUIComponents> rewardOptions;
}


[System.Serializable]
public struct OptionUIComponents
{
    public RewardOption assignedItem;
    public Image iconImage;
    public GameObject isNewText;
    public OptionInfoPanelComponents InfoPanel;
}

[System.Serializable]
public struct OptionInfoPanelComponents
{
    public GameObject infoPanelParent;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI statInfo;
}