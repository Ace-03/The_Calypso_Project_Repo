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

public class RewardOption
{
    public PassiveItemSO itemData;
    public List<StatModifier> modifiers;
    public List<StatChangeDelta> deltaValues;
    public string instanceID;
}

public class StatChangeDelta
{
    public StatType Type;
    public float DeltaValue;
    public StatModifierType ModType;
}
