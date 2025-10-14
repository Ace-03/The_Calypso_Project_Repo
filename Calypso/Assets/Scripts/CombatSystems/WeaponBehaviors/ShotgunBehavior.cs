using UnityEngine;

public class ShotgunBehavior : ParticleWeaponBase, IWeaponBehavior
{
    public override void ApplyWeaponStats(WeaponController weapon)
    {
        StopAttack();
        BurstModifier.SetCount(ps, 0, weapon.GetAmount());
        GeneralModifier.SetDuration(ps, weapon.GetCooldown());
        GeneralModifier.SetLifetime(ps, weapon.GetDuration());

        base.ApplyWeaponStats(weapon);
    }

    public override bool IsAimable()
    {
        return true;
    }
}
