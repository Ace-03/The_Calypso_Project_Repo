using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthSystem
{
    public int maxHP;
    public int hp;
    public int bonusHP;
    public bool invulnerable;

    public void TakeDamage(DamageInfo info)
    {
        Debug.Log("Damage Taken: " + info.damage);

        if (invulnerable) { return; }

        hp -= (int)info.damage;
        
        if (hp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        // Game Over Logic Here

        // stubbed out for now
        Debug.Log("Player Died");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(Vector3.up * 50, ForceMode.Impulse);
        }
    }

    public void Initialize(HealthData data)
    {
        maxHP = data.maxHP;
        hp = data.maxHP;
    }
}

