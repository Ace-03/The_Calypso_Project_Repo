using UnityEngine;

public class BasicWeapon : MonoBehaviour, IWeaponBehavior
{
    private ParticleSystem system;

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();

        var col = system.collision;

        if (transform.parent.CompareTag("Player"))
            col.collidesWith = LayerMask.GetMask("Enemy", "Environment");
        else if (transform.parent.CompareTag("Enemy"))
            col.collidesWith = LayerMask.GetMask("Player", "Environment");
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        GeneralModifier.SetDuration(system, weapon.GetCooldown());
        BurstModifier.SetCount(system, 0, weapon.GetAmount());
    }

    public void Attack(WeaponController weapon)
    {
        if (system != null && !system.isPlaying)
            system.Play();
    }

    public bool IsAimable()
    {
        return false;
    }
}
