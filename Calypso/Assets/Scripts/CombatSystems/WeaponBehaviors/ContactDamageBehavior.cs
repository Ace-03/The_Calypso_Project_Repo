using UnityEngine;

public class ContactDamageBehavior : MonoBehaviour, IWeaponBehavior
{
    private SphereCollider weaponCollider;
    [SerializeField] private BulletTrigger bt;

    private void Awake()
    {
        if (bt == null)
            TryGetComponent<BulletTrigger>(out bt);

        if (!TryGetComponent<SphereCollider>(out weaponCollider))
            weaponCollider = gameObject.AddComponent<SphereCollider>();
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        weaponCollider.radius = weapon.currentStats.Area != 0 ? weapon.currentStats.Area : 0.5f;
        bt.SetDamageSource(weapon.GetDamageSource());
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
