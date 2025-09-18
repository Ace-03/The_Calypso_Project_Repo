using UnityEngine;

public class RifleBehavior : MonoBehaviour, IWeaponBehavior
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
        Debug.Log("Running Rifle Attack");
        if (ps != null && !ps.isPlaying)
        {
            ps.Play();
            Debug.Log("Playing Particle System");
        }
    }
    public void ApplyWeaponStats(WeaponController weapon)
    {
        // Let me know what should go where, even in laymans terms.
        GeneralModifier.SetDamage(bt, 3);
        GeneralModifier.SetRateOverTime(ps, (int)weapon.currentStats["Amount"]);
        GeneralModifier.SetDuration(ps, weapon.currentStats["Cooldown"]);
        GeneralModifier.SetLifetime(ps, weapon.currentStats["Duration"]);
    }
}
