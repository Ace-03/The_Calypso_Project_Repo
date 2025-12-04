using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField]
    private OnDamageDealtEventSO damageDealtEvent;
    [SerializeField]
    private OnDamageDealtEventSO damageTakenEvent;

    [SerializeField] private bool enemyTickDamage;
    private float tickInterval = 1f;
    private bool isTicking = false;
    private float tickTimer = 0f;

    [HideInInspector] public WeaponDefinitionSO weaponData;
    [HideInInspector] public EnemyDefinitionSO enemyData;

    private void Awake()
    {
        // assumes info is attatched to same object.
        weaponData = GetComponentInParent<WeaponController>()?.GetWeaponData();
        enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();
    }

    private void Update()
    {
        if (!isTicking) { return; }

        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0f) { isTicking = false; }
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

        if (enemyTickDamage && !isTicking)
        {
            TryEnemyDamage(other.gameObject);
            isTicking = true;
            tickTimer = tickInterval;
        }
    }

    private void TryDamage(GameObject other, bool isExternal = false)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamagePayload payload = MakePayload(other);

            if (other.CompareTag("Player"))
            {
                payload.damageInfo = DamageCalculator.GetDamageToPlayer(enemyData);
                damageTakenEvent.Raise(payload);

            }
            else if (other.CompareTag("Enemy"))
            {
                payload.damageInfo = DamageCalculator.GetDamageFromPlayer(weaponData);
                damageDealtEvent.Raise(payload);

            }

            other.GetComponent<GenericHealth>().TakeDamage(payload.damageInfo);
        }
    }

    private void TryPlayerDamage(GameObject other)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamagePayload payload = MakePayload(other);

            if (other.CompareTag("Player"))
            {
                if (enemyData == null)
                    enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();

                payload.damageInfo = DamageCalculator.GetDamageToPlayer(enemyData);
                damageTakenEvent.Raise(payload);

                other.GetComponent<GenericHealth>().TakeDamage(payload.damageInfo);
            }
        }
    }

    private void TryEnemyDamage(GameObject other)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamagePayload payload = MakePayload(other);

            if (other.CompareTag("Enemy"))
            {
                payload.damageInfo = DamageCalculator.GetDamageFromPlayer(weaponData);
                damageDealtEvent.Raise(payload);

                other.GetComponent<GenericHealth>().TakeDamage(payload.damageInfo);
            }
        }
    }

    private DamagePayload MakePayload(GameObject other)
    {
        return new DamagePayload()
        {
            damageInfo = new DamageInfo(),
            attacker = gameObject,
            receiver = other.gameObject,
        };
    }

    public void SetData(WeaponDefinitionSO weapon)
    {
        weaponData = weapon;
    }

    public void SetData(EnemyDefinitionSO enemy)
    {
        enemyData = enemy;
    }

    public void SetTickInterval(float interval)
    {
        tickInterval = interval;
    }

    public void TryExternalDamage(GameObject other)
    {
        TryDamage(other, true);
    }
}
