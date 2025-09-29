using UnityEngine;


[CreateAssetMenu(fileName = "NewPickupDefinition", menuName = "PickupSO")]
public class PickupSO : ScriptableObject
{
    public string pickupName;
    public int pickupValue;
    public Sprite sprite;
    public GameObject prefab;
    public ResourceType resourceType;
}
