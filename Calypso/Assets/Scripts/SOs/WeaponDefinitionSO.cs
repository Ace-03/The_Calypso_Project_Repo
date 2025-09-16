using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponDefinition", menuName = "WeaponSO")]
public class WeaponDefinitionSO : ScriptableObject
{
    [Header("Weapon Base Stats")]
    public string weaponName;
    public GameObject weaponBehaviorPrefab;
    public Sprite icon;
    public float baseCooldown;
    public float baseDamage;
    public float baseProjectileSpeed;
    public float baseDuration;
    public float baseAmmount;
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
