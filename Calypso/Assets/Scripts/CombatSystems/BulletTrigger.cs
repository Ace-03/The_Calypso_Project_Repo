using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField]
    private OnDamageDealtEventSO damageDealtEvent;
    [SerializeField]
    private OnDamageDealtEventSO damageTakenEvent;

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

    private void OnTriggerStay(Collider other)
    {
        TryPlayerDamage(other.gameObject);
    }

    private void TryDamage(GameObject other, bool isExternal = false)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamageInfo damageInfo = new DamageInfo();

            DamagePayload damagePayload = new DamagePayload()
            {
                damageInfo = damageInfo,
                attacker = this.gameObject,
                receiver = other.gameObject,
            };

            if (other.CompareTag("Player"))
            {
                if (enemyData == null)
                    enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();

                damageTakenEvent.Raise(damagePayload);

                damageInfo = DamageCalculator.GetDamageToPlayer(enemyData);
            }
            else if (other.CompareTag("Enemy"))
            {
                damageDealtEvent.Raise(damagePayload);

                damageInfo = DamageCalculator.GetDamageFromPlayer(weaponData);
            }

            other.GetComponent<GenericHealth>().TakeDamage(damageInfo);

        }
    }

    private void TryPlayerDamage(GameObject other)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamageInfo damageInfo = new DamageInfo();

            DamagePayload damagePayload = new DamagePayload()
            {
                damageInfo = damageInfo,
                attacker = this.gameObject,
                receiver = other.gameObject,
            };

            if (other.CompareTag("Player"))
            {
                if (enemyData == null)
                    enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();

                damageTakenEvent.Raise(damagePayload);

                damageInfo = DamageCalculator.GetDamageToPlayer(enemyData);
            }
        }
    }

    public void TryExternalDamage(GameObject other)
    {
        TryDamage(other, true);
    }
}
