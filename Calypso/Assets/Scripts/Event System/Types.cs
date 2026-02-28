using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameEventPayload
{
}

public class SelectedRewardPayload : GameEventPayload
{
    public RewardOption option;
}

public class DamagePayload : GameEventPayload
{
    public DamageInfo damageInfo;
    public GameObject attacker;
    public GameObject receiver;
    public Vector3 hitPosition;
}

public class DeathPayload : GameEventPayload
{
    public GameObject entity;
}

public class RewardOptionsPayload : GameEventPayload
{
    public List<RewardOption> options;
}

public class BlueprintCollectedPayload : GameEventPayload
{
    public string weaponName;
}

public class StatUpdatePayload : GameEventPayload
{
    public StatSystem statSystem;

    public StatUpdatePayload(StatSystem statSystem)
    {
        this.statSystem = statSystem;
    }
}

public class RespawnScenePayload : GameEventPayload
{
    public bool gambleResources;
}

public class UpdateHotBarPayload : GameEventPayload
{
    public List<WeaponDefinitionSO> currentWeapons;
    public List<PassiveItemSO> passiveItems;

    public UpdateHotBarPayload(List<WeaponDefinitionSO> currentWeapons, List<PassiveItemSO> passiveItems)
    {
        this.currentWeapons = currentWeapons;
        this.passiveItems = passiveItems;
    }
}

public class WeaponCraftedPayload : GameEventPayload
{
    public WeaponDefinitionSO weaponData;
}

public class WeaponsUpdatePayload : GameEventPayload
{
    public List<WeaponDefinitionSO> weapons;
}

public class CraftingAttemptPayload : GameEventPayload
{
    public WeaponRecipeSO weaponRecipe;
}

public class DayStateChangePayload : GameEventPayload
{
    public bool isDayTime;
    public int dayCount;

    public DayStateChangePayload(bool isDayTime, int dayCount)
    {
        this.isDayTime = isDayTime;
        this.dayCount = dayCount;
    }
}

public class RewardOption
{
    public PassiveItemSO itemData;
    public List<StatModifier> modifiers;
    public List<StatChangeDelta> deltaValues;
    public string instanceID;
    public bool isNew;
}

public class StatChangeDelta
{
    public StatType Type;
    public float oldValue;
    public float newValue;
    public float delta;
    public StatModifierType ModType;
}