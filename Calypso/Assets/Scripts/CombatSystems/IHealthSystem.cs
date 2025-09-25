public interface IHealthSystem
{
    void TakeDamage(DamageInfo info);

    void Die();

    void Initialize(HealthData data);
}
