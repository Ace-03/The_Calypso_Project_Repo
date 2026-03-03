using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

[Serializable]
public class HotBarUIElements
{
    public HotBar WeaponBar;
    public HotBar PassiveBar;
}

[Serializable]
public class HotBar
{
    public GameObject BarParent;
    public List<HotBarSlotController> Slots;
}

[Serializable]
public class HealthUIElements
{
    public Image healthIcon;
    public Image baseIcon;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI backgroundHealthText;
    public TextMeshProUGUI baseText;

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

    public Transform boatIconContainer;
    public GameObject boatIconPrefab;
}

[Serializable]
public class LevelUIElements
{
    public TextMeshProUGUI levelText;
}

[Serializable]
public class SlotComponents
{
    [HideInInspector] public PassiveItemSO assignedPassiveItem;
    [HideInInspector] public WeaponController assignedWeapon;
    public GameObject slotObject;
    public Image backgroundImage;
    public Image iconImage;
}

[Serializable]
public class DayTimerUIElements
{
    public OnDayStateChangeEventSO dayStateChangeEvent;

    public int maxTimeLineWaves = 11;
    public Sprite sunSprite;
    public Sprite moonSprite;
    public Image TimelineStartIcon;
    public Image TimelineEndIcon;
    public GameObject TimelineWavePrefab;
    public Transform timeLineContainer;
    public TextMeshProUGUI dayCountText;
}