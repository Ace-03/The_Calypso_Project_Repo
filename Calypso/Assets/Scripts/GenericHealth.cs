using UnityEngine;

public class GenericHealth : MonoBehaviour, IHealthSystem
{
    [SerializeField]
    public int maxHP;
    public int hp;

    private void Awake()
    {
        Initialize(new HealthData { maxHP = maxHP });
    }

    public void TakeDamage(DamageInfo info)
    {
        TakeDamageRaw((int)info.damage);
    }

    public void TakeDamageRaw(int damage)
    {
        hp -= (int)damage;
        
        if (hp <= 0)
            Die();
    }
    
    public void Die()
    {
        hp = maxHP;
        Destroy(gameObject);
    }
    
    public int GetMaxHealth()
    {
        return maxHP;
    }

    public void Initialize(HealthData data)
    {
        maxHP = data.maxHP;
        hp = maxHP;
    }
}
