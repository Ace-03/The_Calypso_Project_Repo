using UnityEngine;

public class ShotgunBehavior : MonoBehaviour, IWeaponBehavior
{
    public ParticleSystem ps;
    public BulletTrigger bt;
    public WeaponController controller;

    private void Start()
    {
        Attack(controller);
    }
    public void Attack(WeaponController weapon)
    {
        ApplyWeaponStats(weapon);
        if (ps != null)
        {
            if (!ps.isPlaying)
            {
                ps.Play();
            }
        }
    }
    public void ApplyWeaponStats(WeaponController weapon)
    {
        // Let me know what should go where, even in laymans terms.
        WeaponDefinitionSO weaponData = weapon.GetWeaponData();
        GeneralModifier.SetDamage(bt, 3);
        BurstModifier.SetCount(ps, 0, (int)weaponData.baseAmmount);
        GeneralModifier.SetDuration(ps, weaponData.baseCooldown);
        GeneralModifier.SetLifetime(ps, weaponData.baseDuration);
        //GeneralModifier.SetDamage(bt, (int)weapon.GetWeaponData().baseDamage);
    }
}
