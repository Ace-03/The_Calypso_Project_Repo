using UnityEngine;

public class BurstRifleBehavior : MonoBehaviour, IWeaponBehavior
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
            BurstModifier.SetCycles(ps, 0, weapon.GetAmount());
             BurstModifier.SetInterval(ps, 0, weapon.GetCooldown() / weapon.GetAmount());
            GeneralModifier.SetDuration(ps, weapon.GetCooldown()*2);
            GeneralModifier.SetLifetime(ps, weapon.GetDuration());
            //GeneralModifier.SetDamage(bt, (int)weapon.GetWeaponData().baseDamage);
    }
}
