using UnityEngine;

public class BurstRifleBehavior : MonoBehaviour, IWeaponBehavior
{
    private ParticleSystem ps;
    
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        var col = ps.collision;

        if (transform.parent.CompareTag("Player"))
            col.collidesWith = LayerMask.GetMask("Enemy", "Environment");
        else if (transform.parent.CompareTag("Enemy"))
            col.collidesWith = LayerMask.GetMask("Player", "Environment");
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

        BurstModifier.SetCycles(ps, 0, weapon.GetAmount());
        BurstModifier.SetInterval(ps, 0, weapon.GetCooldown() / weapon.GetAmount());
        GeneralModifier.SetDuration(ps, weapon.GetCooldown()*2);
        GeneralModifier.SetLifetime(ps, weapon.GetDuration());
        GeneralModifier.SetSpeed(ps, weapon.GetSpeed() * 10);
        GeneralModifier.SetCircleArc(ps, 1 / weapon.GetAccuracy() * 200);
    }

    public bool IsAimable()
    {
        return true;
    }
}
