using UnityEngine;

public class ShotgunBehavior : MonoBehaviour, IWeaponBehavior
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

            BurstModifier.SetCount(ps, 0, weapon.GetAmount());
            GeneralModifier.SetDuration(ps, weapon.GetCooldown());
            GeneralModifier.SetLifetime(ps, weapon.GetDuration());
    }

    public bool IsAimable()
    {
        return true;
    }
}
