using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletTrigger))]
[RequireComponent(typeof(ParticleSystem))]
public class ParticleWeaponBase : MonoBehaviour
{
    public ParticleSystem ps;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public virtual void Attack(WeaponController weapon)
    {
        if (ps != null && !ps.isPlaying)
            ps.Play();
    }
    public virtual void ApplyWeaponStats(WeaponController weapon)
    {
        StopAttack();
        ApplyModifiers(weapon, ps);
    }

    public virtual void ApplyWeaponStats(WeaponController weapon, List<ParticleSystem> particle)
    {
        StopAttack();

        foreach (ParticleSystem ps in particle)
        {
            ApplyModifiers(weapon, ps);
        }
    }

    private void ApplyModifiers(WeaponController weapon, ParticleSystem ps)
    {
        GeneralModifier.SetSprite(ps, weapon.GetSprite());
        GeneralModifier.UpdateTeam(ps, weapon.team);
    }

    public virtual bool IsAimable()
    {
        return true;
    }
    public virtual void StopAttack()
    {
        if (ps != null && ps.isPlaying)
            ps.Stop();
    }
}
public enum TEAM
{
    Player,
    Enemy,
    PlayerPierce
}
