using UnityEngine;

public class BasicWeapon : MonoBehaviour, IWeaponBehavior
{
    private ParticleSystem system;

    private void Start()
    {
        system = GetComponent<ParticleSystem>();
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {

    }

    public void Attack(WeaponController weapon)
    {
        system.Play();
    }
}
