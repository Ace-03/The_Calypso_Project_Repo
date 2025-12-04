using System.Collections.Generic;
using UnityEngine;

public class TripleBurstRifle : BaseParticleWeapon, IWeaponBehavior
{
    [SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();
    [SerializeField] private List<BulletTrigger> bulletTriggers = new List<BulletTrigger>();

    public override void ApplyWeaponStats(WeaponController weapon)
    {
        StopAttack();

        foreach (ParticleSystem ps in particles)
        {
            BurstModifier.SetCycles(ps, 0, weapon.GetAmount());
            BurstModifier.SetInterval(ps, 0, weapon.GetCooldown() / weapon.GetAmount());
            GeneralModifier.SetDuration(ps, weapon.GetCooldown() * 2);
            GeneralModifier.SetLifetime(ps, weapon.GetDuration());
            GeneralModifier.SetSpeed(ps, weapon.GetSpeed() * 10);
            GeneralModifier.SetCircleArc(ps, 1 / weapon.GetAccuracy() * 200);
        }

        foreach (BulletTrigger bt in bulletTriggers)
        {
            bt.SetData(weapon.GetWeaponData());
            bt.weaponData = weapon.GetWeaponData();
        }

        base.ApplyWeaponStats(weapon, particles);
    }

    public override bool IsAimable()
    {
        return true;
    }
}
