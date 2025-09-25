using UnityEngine;

public class BasicWeapon : MonoBehaviour, IWeaponBehavior
{
    private ParticleSystem system;

    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
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
