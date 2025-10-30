using System.Collections.Generic;
using UnityEngine;

public class TripleBurstRifle : ParticleWeaponBase, IWeaponBehavior
{
    [SerializeField] private List<ParticleSystem> particles = new List<ParticleSystem>();

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

        base.ApplyWeaponStats(weapon, particles);
    }

    public override bool IsAimable()
    {
        return true;
    }
}
