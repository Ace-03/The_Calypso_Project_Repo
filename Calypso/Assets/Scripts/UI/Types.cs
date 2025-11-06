using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct RewardScreenUIComponents
{
    public GameObject RewardScreenParent;
    public GameObject RewardsContainer;
    public List<RewardOptionUIComponents> rewardOptions;
}


[System.Serializable]
public struct RewardOptionUIComponents
{
    public RewardOption assignedItem;
    public Image iconImage;
    public GameObject isNewText;
    public RewardInfoPanelComponents rewardInfoPanel;
}

[System.Serializable]
public struct RewardInfoPanelComponents
{
    public GameObject infoPanelParent;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI statInfo;
}