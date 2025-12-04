using UnityEngine;

public class BasicWeapon : BaseParticleWeapon, IWeaponBehavior
{
    private ParticleSystem system;

    public override void ApplyWeaponStats(WeaponController weapon)
    {
        GeneralModifier.SetDuration(system, weapon.GetCooldown());
        BurstModifier.SetCount(system, 0, weapon.GetAmount());

        base.ApplyWeaponStats(weapon);
    }

    public override bool IsAimable()
    {
        return false;
    }
}
