using UnityEngine;

public class BurstRifleBehavior : BulletScript, IWeaponBehavior
{

    public override void ApplyWeaponStats(WeaponController weapon)
    {
        StopAttack();

        BurstModifier.SetCycles(ps, 0, weapon.GetAmount());
        BurstModifier.SetInterval(ps, 0, weapon.GetCooldown() / weapon.GetAmount());
        GeneralModifier.SetDuration(ps, weapon.GetCooldown()*2);
        GeneralModifier.SetLifetime(ps, weapon.GetDuration());
        GeneralModifier.SetSpeed(ps, weapon.GetSpeed() * 10);
        GeneralModifier.SetCircleArc(ps, 1 / weapon.GetAccuracy() * 200);
    }

    public override bool IsAimable()
    {
        return true;
    }
}
