using UnityEngine;

public class ContactDamageBehavior : MonoBehaviour, IWeaponBehavior
{
    private SphereCollider weaponCollider;

    private void Awake()
    {
        if (!TryGetComponent<SphereCollider>(out weaponCollider))
            weaponCollider = gameObject.AddComponent<SphereCollider>();
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        weaponCollider.radius = weapon.currentStats.ContainsKey("Area") ? weapon.currentStats["Area"] : 0.5f;
    }

    public void Attack(WeaponController weapon)
    {
        return;
    }

    public bool IsAimable()
    {
        return false;
    }
}
