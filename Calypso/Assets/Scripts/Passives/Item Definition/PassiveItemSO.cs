using UnityEngine;

[CreateAssetMenu(fileName = "NewPassiveDefinition", menuName = "PassiveItemSO")]
public abstract class PassiveItemSO : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public float itemBaseValue;
    public IItemEffect itemBehavior;
}