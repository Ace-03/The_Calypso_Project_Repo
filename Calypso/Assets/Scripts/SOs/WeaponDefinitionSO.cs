using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponDefinition", menuName = "Scriptable Objects/WeaponSO")]
public class WeaponDefinitionSO : ScriptableObject
{
    [Header("Weapon Base Stats")]
    public string weaponName;
    public GameObject weaponBehaviorPrefab;
    public Material bulletSprite;
    public float baseCooldown;
    public float baseDamage;
    public float baseProjectileSpeed;
    public float baseAccuracy;
    public float baseDuration;
    public float baseAmount;
    public int requiredResources;

    [Header("AOE Stats")]
    [SerializeField]
    private bool hasAOE;
    public float aoeAreaSize;
    public float aoeTickRate;
    public AOEShape aoeShape;

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
