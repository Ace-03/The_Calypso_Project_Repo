using UnityEngine;

public class GameEventPayload
{
}

public class RewardPayload : GameEventPayload
{

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

public class RewardOption : GameEventPayload
{
    public PassiveItemSO itemData;
    public float itemValueIncrease;
}
