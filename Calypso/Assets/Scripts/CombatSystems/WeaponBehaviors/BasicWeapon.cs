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
        GeneralModifier.SetDuration(system, weapon.currentStats["Cooldown"]);
        BurstModifier.SetCount(system, 0, (int)weapon.currentStats["Amount"]);
    }

    public void Attack(WeaponController weapon)
    {
        if (system != null && !system.isPlaying)
            system.Play();
    }
}
