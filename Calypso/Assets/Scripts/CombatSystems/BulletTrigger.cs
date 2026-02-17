using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    [SerializeField] private OnDamageDealtEventSO damageDealtEvent;
    [SerializeField] private OnDamageDealtEventSO damageTakenEvent;

    [SerializeField] private bool enemyTickDamage;
    [SerializeField] private float tickInterval = 0.75f;
    private bool isTicking = false;
    private float tickTimer = 0f;

    [HideInInspector] private DamageSource damageSource = new DamageSource();


    private void Update()
    {
        if (!isTicking) { return; }

        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0f) { isTicking = false; }
    }

    void OnParticleCollision(GameObject other) => TryDamage(other);

    private void OnTriggerEnter(Collider other) => TryDamage(other.gameObject);

    private void OnTriggerStay(Collider other) => TryDamage(other.gameObject, damageType.tick);

    private void TryDamage(GameObject other, damageType type = damageType.normal)
    {
        if (other.GetComponent<GenericHealth>() != null)
        {
            DamagePayload payload = MakePayload(other);

            if (other.CompareTag("Player"))
            {
                payload.damageInfo = DamageCalculator.CalculateDamageToPlayer(damageSource);
                damageTakenEvent.Raise(payload);
            }
            else if (other.CompareTag("Enemy"))
            {
                if (type == damageType.tick)
                {
                    if (isTicking)
                    {
                        return;
                    }
                    else
                    {
                        Debug.Log("enemy taking tick damage");
                        isTicking = true;
                        tickTimer = tickInterval;
                    }
                }

                payload.damageInfo = DamageCalculator.CalculateDamageToEnemy(damageSource);
                damageDealtEvent.Raise(payload);
            }
            other.GetComponent<GenericHealth>().TakeDamage(payload.damageInfo);
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

    public void TryExternalDamage(GameObject other) => TryDamage(other, damageType.external);
    public void SetTickInterval(float interval) => tickInterval = interval;
    public void SetDamageSource(DamageSource src) => damageSource = src;

    enum damageType
    {
        external,
        tick,
        normal
    }
}
