using UnityEngine;

public class ShotgunBehavior : BulletScript, IWeaponBehavior
{
    public override void ApplyWeaponStats(WeaponController weapon)
    {
        StopAttack();
        BurstModifier.SetCount(ps, 0, weapon.GetAmount());
        GeneralModifier.SetDuration(ps, weapon.GetCooldown());
        GeneralModifier.SetLifetime(ps, weapon.GetDuration());
    }

    public override bool IsAimable()
    {
        return true;
    }
}
