using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public class HotBarUIElements
{
    public Sprite emptySlotSprite;
    public GameObject hotBarSlotPrefab;


    public HotBar WeaponBar;
    public HotBar PassiveBar;

    public Sprite LockedBarSprite;
    public Sprite UnlockedBarSprite;
}

[Serializable]
public class HotBar
{
    public GameObject BarParent;
    public List<HotBarSlot> Slots;
}

[Serializable]
public class HealthUIElements
{
    public Image healthIcon;
    public Image baseIcon;

    public List<Sprite> playerHealthSprites;
    public List<Sprite> baseHealthSprites;
}

[Serializable]

public class ResourceUIElements
{
    public TextMeshProUGUI ironText;
    public TextMeshProUGUI stoneText;

    public Sprite EmptyBoatSprite;
    public Sprite aquiredBoatSprite;

    public List<Image> boatIcons;
}

[Serializable]
public class LevelUIElements
{
    public TextMeshProUGUI levelText;
}

[Serializable]
public class HotBarSlot
{
    public PassiveItemSO assignedPassiveItem;
    public WeaponController assignedWeapon;
    public GameObject slotObject;
    public Image backgroundImage;
    public Image iconImage;
    public bool isFilled;
    public bool isLocked;
}
