using UnityEngine;

public class DamageCalculator : MonoBehaviour
{
    public static DamageCalculator Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public DamageInfo GetDamageInfo(WeaponDefinitionSO weaponData)
    {
        return new DamageInfo
        {
            damage = weaponData.baseDamage + (weaponData.baseDamage * (PlayerStats.Instance.GetStrength() / 100)),
            // remaining values to be added.
        };

    }
}

public class DamageInfo
{
    public float damage;
    public float knockbackStrength;
    public float stunDuration;
    public float poisonDuration;
    public float slowDuration;
}

