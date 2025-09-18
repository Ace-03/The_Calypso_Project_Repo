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
            BurstModifier.SetCount(ps, 0, (int)weapon.currentStats["Amount"]);
            GeneralModifier.SetDuration(ps, weapon.currentStats["Cooldown"]);
            GeneralModifier.SetLifetime(ps, weapon.currentStats["Duration"]);
            //GeneralModifier.SetDamage(bt, (int)weapon.GetWeaponData().baseDamage);
    }
}
