using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    WeaponDefinitionSO weaponData;
    EnemyDefinitionSO enem;

    private void Start()
    {
        // assumes info is attatched to same object.
        weaponData = GetComponentInParent<WeaponController>()?.GetWeaponData();
        enem = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();
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
        if (other.GetComponent<IHealthSystem>() != null)
        {
            DamageInfo damageInfo = new DamageInfo();

            if (other.CompareTag("Player"))
                damageInfo = DamageCalculator.GetDamageInfo(enem);
            else
                damageInfo = DamageCalculator.GetDamageInfo(weaponData);

            other.GetComponent<IHealthSystem>().TakeDamage(damageInfo);
        }
    }
}
