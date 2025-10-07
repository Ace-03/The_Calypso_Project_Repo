using UnityEngine;

public class BasicWeapon : BulletScript, IWeaponBehavior
{
    private ParticleSystem system;

    public override void ApplyWeaponStats(WeaponController weapon)
    {
        GeneralModifier.SetDuration(system, weapon.GetCooldown());
        BurstModifier.SetCount(system, 0, weapon.GetAmount());
    }

    public override bool IsAimable()
    {
        return false;
    }
}
