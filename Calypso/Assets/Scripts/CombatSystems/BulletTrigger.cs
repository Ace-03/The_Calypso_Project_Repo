using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    WeaponDefinitionSO weaponData;
    public int bulletDamage = 1; // stubbed for now

    private void Start()
    {
        // assumes weapon controller is attatched to same object for now.
        // likely will change.
        weaponData = GetComponentInParent<WeaponController>()?.GetWeaponData();
    }

    
    void OnParticleCollision(GameObject other)
    {
        TryDamage(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other.gameObject);
    }

    private void TryDamage(GameObject other)
    {
        if (other.GetComponent<HealthSystem>() != null)
        {
            DamageInfo damageInfo = new DamageInfo();

            if (other.CompareTag("Player"))
                damageInfo.damage = bulletDamage; // stubbed for now
            else
                damageInfo = DamageCalculator.GetDamageInfo(weaponData);

            other.GetComponent<HealthSystem>().TakeDamage(damageInfo);
        }
    }
}
