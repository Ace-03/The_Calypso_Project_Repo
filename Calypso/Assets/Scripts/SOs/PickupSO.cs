using UnityEngine;


[CreateAssetMenu(fileName = "NewPickupDefinition", menuName = "Scriptable Objects/Pickup")]
public class PickupSO : ScriptableObject
{
    public string pickupName;
    public string pickupType;
    public int pickupValue;
    public Sprite sprite;
    public float sizeModifier = 1f;
    public GameObject prefab;

    [Header("Weapon Data For Blueprint")]
    public WeaponDefinitionSO weaponToUnlock;
}

