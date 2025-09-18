using UnityEngine;

public class ShotgunBehavior : MonoBehaviour, IWeaponBehavior
{
    private ParticleSystem ps;
    private BulletTrigger bt;
    
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        bt = GetComponent<BulletTrigger>();
    }
    public void Attack(WeaponController weapon)
    {
        if (ps != null && !ps.isPlaying)
            ps.Play();
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        if (ps != null && ps.isPlaying)
            ps.Stop();

            // Let me know what should go where, even in laymans terms.
            GeneralModifier.SetDamage(bt, 3);
            BurstModifier.SetCount(ps, 0, weapon.GetAmount());
            GeneralModifier.SetDuration(ps, weapon.GetCooldown());
            GeneralModifier.SetLifetime(ps, weapon.GetDuration());
            //GeneralModifier.SetDamage(bt, (int)weapon.GetWeaponData().baseDamage);
    }
}
