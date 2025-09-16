using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private WeaponDefinitionSO weaponData;

    private IWeaponBehavior weaponBehavior;
    private float cooldown;

    private bool hasAOE;
    private float area;
    private float aoeTick;

    private float cooldownTimer;


    private void Start()
    {
        InitializeData();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            Attack();
            cooldownTimer = cooldown;
        }
    }

    public void InitializeData()
    {
        weaponBehavior = weaponData.weaponBehaviorPrefab.GetComponent<IWeaponBehavior>();
        cooldown = weaponData.baseCooldown;

        if (weaponData.hasAOE)
        {
            hasAOE = true;
            area = weaponData.aoeAreaSize;
            aoeTick = weaponData.aoeTickRate;
        }
    }

    /* ApplyPassiveModifiers exists to apply passive item
     * effects to the weapon. 
     * 
     * the number of passive items and specific effects are
     * unknown. To approach this there will likely be a
     * "passives controller" script which holds an array of
     * passive items. each passive will have an interface with
     * a "ApplyModifier(WeaponController weapon)" method that 
     * will do the work to modify the stats.
     * 
     */
    public void ApplyPassiveModifiers()
    {

    }

    public void ApplyPlayerModifiers()
    {
        cooldown = weaponData.baseCooldown + (weaponData.baseCooldown * (PlayerStats.Instance.GetCooldown() / 100));

        if (hasAOE)
        {
            aoeTick = weaponData.aoeTickRate + (weaponData.aoeTickRate * (PlayerStats.Instance.GetCooldown() / 100));
            area = weaponData.aoeAreaSize + (weaponData.aoeAreaSize * (PlayerStats.Instance.GetArea() / 100));
        }
    }

    public void Attack()
    {
        weaponBehavior?.Attack(this);
    }

    public WeaponDefinitionSO GetWeaponData()
    {
        return weaponData;
    }
}
