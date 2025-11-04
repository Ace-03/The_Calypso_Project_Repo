using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public struct HotBarUIElements
{
    public GameObject hotBarSlotPrefab;

    public HotBar WeaponBar;
    public HotBar PassiveBar;

    public Sprite LockedBarSprite;
    public Sprite UnlockedBarSprite;
}

[Serializable]
public struct HotBar
{
    public GameObject BarParent;
    public List<HotBarSlot> Slots;
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
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI stoneText;

    public Sprite EmptyBoatSprite;
    public Sprite aquiredBoatSprite;

    public List<Image> boatIcons;
}

[Serializable]
public struct LevelUIElements
{
    public TextMeshProUGUI levelText;
}

[Serializable]
public struct HotBarSlot
{
    public PassiveItemSO assignedPassiveItem;
    public WeaponController assignedWeapon;
    public GameObject slotObject;
    public Image backgroundImage;
    public Image iconImage;
    public bool isFilled;
    public bool isLocked;
}
