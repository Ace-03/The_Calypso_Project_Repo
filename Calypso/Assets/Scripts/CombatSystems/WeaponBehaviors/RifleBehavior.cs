using UnityEngine;

public class RifleBehavior : BulletScript, IWeaponBehavior
{
    public override void ApplyWeaponStats(WeaponController weapon)
    {
        GeneralModifier.SetRateOverTime(ps, weapon.GetAmount());
        GeneralModifier.SetDuration(ps, weapon.GetCooldown());
        GeneralModifier.SetLifetime(ps, weapon.GetDuration());
        GeneralModifier.SetCircleArc(ps, 1 / weapon.GetAccuracy() * 200);
    }

    public override bool IsAimable()
    {
        return true;
    }
}
