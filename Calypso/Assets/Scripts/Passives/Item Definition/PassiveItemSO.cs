using UnityEngine;

public abstract class PassiveItemSO : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public float itemBaseValue;
    public IItemEffect itemBehavior;
}