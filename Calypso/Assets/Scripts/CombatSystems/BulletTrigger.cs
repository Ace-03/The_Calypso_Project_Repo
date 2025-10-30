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

    private void TryDamage(GameObject other, bool isExternal = false)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamageInfo damageInfo = new DamageInfo();

            if (other.CompareTag("Player") && !isExternal)
            {
                if (enemyData == null)
                    enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();

                damageInfo = DamageCalculator.GetDamageToPlayer(enemyData);
                other.GetComponent<GenericHealth>().TakeDamage(damageInfo);
            }
            else if (other.CompareTag("Enemy"))
            {
                damageInfo = DamageCalculator.GetDamageFromPlayer(weaponData);
                other.GetComponent<GenericHealth>().TakeDamage(damageInfo);
            }
        }
    }
    public void TryExternalDamage(GameObject other)
    {
        TryDamage(other, true);
    }
}
