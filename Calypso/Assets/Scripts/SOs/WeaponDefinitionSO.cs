using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponDefinition", menuName = "WeaponSO")]
public class WeaponDefinitionSO : ScriptableObject
{
    public string weaponName;
    public GameObject weaponBehaviorPrefab;
    public Sprite icon;
    public float baseCooldown;
    public float baseDamage;
    public float baseProjectileSpeed;
    public float baseDuration;
    public float baseAmmount;

    public bool hasAOE;
    public float aoeAreaSize;
    public float aoeTickRate;
    public AOEShape aoeShape;
}

public enum AOEShape
{
    Circle,
    Square,
}
