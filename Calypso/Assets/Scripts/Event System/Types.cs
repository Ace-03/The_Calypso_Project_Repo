
using UnityEngine;

public class RewardPayload
{

}

public class DamagePayload
{
    public DamageInfo damageInfo;
    public GameObject attacker;
    public GameObject receiver;
    public Vector3 hitPosition;
}

public class DeathPayload
{
    public GameObject entity;
}

public class RewardOption
{
    public PassiveItemSO itemData;
    public float itemValueIncrease;
}