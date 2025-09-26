public interface IHealthSystem
{
    void TakeDamage(DamageInfo info);

    void TakeDamageRaw(int damage);

    int GetMaxHealth();

    void Die();

    void Initialize(HealthData data);
}
