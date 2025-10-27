using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public struct HotBarUIElements
{
    public GameObject hotBarSlotPrefab;

    public GameObject weaponBar;
    public List<HotBarIcon> weaponSlots;

    public GameObject passivesBar;
    public List<HotBarIcon> passiveSlots;
}

[Serializable]
public struct HealthUIElements
{
    public Image healthIcon;
    public Image baseIcon;

    public List<Sprite> playerHealthSprites;
    public List<Sprite> baseHealthSprites;
}

[Serializable]

public struct ResourceUIElements
{
    public TextMeshProUGUI resourceText;

    public List<Image> boatIcons;
}

[Serializable]
public struct LevelUIElements
{
    public TextMeshProUGUI levelText;
}

[Serializable]
public struct HotBarIcon
{
    public GameObject slotObject;
    public Image backgroundImage;
    public Image iconImage;
}
