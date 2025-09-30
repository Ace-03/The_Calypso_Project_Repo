using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    WeaponDefinitionSO weaponData;
    EnemyDefinitionSO enemyData;

    private void Awake()
    {
        // assumes info is attatched to same object.
        weaponData = GetComponentInParent<WeaponController>()?.GetWeaponData();
        enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();
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
            Debug.Log("Has Health System");
            DamageInfo damageInfo = new DamageInfo();

            if (other.CompareTag("Player"))
            {
                Debug.Log("Damaging Player");
                if (enemyData == null)
                    enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();

                damageInfo = DamageCalculator.GetDamageToPlayer(enemyData);
                other.GetComponent<IHealthSystem>().TakeDamage(damageInfo);
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("Damaging Enemy");
                damageInfo = DamageCalculator.GetDamageFromPlayer(weaponData);
                other.GetComponent<IHealthSystem>().TakeDamage(damageInfo);
            }
        }
    }
}
