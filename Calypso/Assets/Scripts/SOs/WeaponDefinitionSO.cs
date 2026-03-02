using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponDefinition", menuName = "Scriptable Objects/Weapon")]
public class WeaponDefinitionSO : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName;
    public string weaponDescription;
    public Sprite icon;
    public Sprite blueprintIcon;

    [Header("Weapon Base Stats")]
    public GameObject weaponBehaviorPrefab;
    public Material bulletSprite;
    public WeaponStats baseStats;
    public bool pierce;

    [Header("Status Effects")]
    public float baseKnockback;
    public float baseStun;
    public float basePoison;
    public float baseSlowdown;
}

public enum AOEShape
{
    Circle,
    Square,
}
