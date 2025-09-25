using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealthSystem
{
    private int maxHP;
    private int hp;
    private int bonusHP;
    private bool invulnerable;

    private float invulnerabilityduration = 0.5f;
    private float invulnerabilityTimer;

    private void Update()
    {
        if (!invulnerable) { return; }

        invulnerabilityTimer -= Time.deltaTime;

        if (invulnerabilityTimer <= 0)
            invulnerable = false;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (invulnerable) { return; }

        Debug.Log("Damage Taken by player: " + info.damage);
        if (bonusHP > 0) 
        {
            int damageToBonus = Mathf.Min(bonusHP, (int)info.damage);
            bonusHP -= damageToBonus;
            info.damage -= damageToBonus;
        }

        hp -= (int)Mathf.Clamp(info.damage, 0f, maxHP);
        invulnerabilityTimer = invulnerabilityduration;
        invulnerable = true;


        if (hp <= 0)
            Die();
    }

    public void Die()
    {
        // Game Over Logic Here

        // stubbed out for now
        Debug.Log("Player Died");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(Vector3.up * 1000, ForceMode.Impulse);
        }
    }

    public void heal(int amount)
    {
        hp += amount;
        if (hp > maxHP + bonusHP)
            hp = maxHP + bonusHP;
    }

    public void addBonusHP(int amount)
    {
        bonusHP += amount;
    }

    public void Initialize(PlayerHealthData data)
    {
        invulnerabilityduration = data.invulnerabilityDuration;
        maxHP = data.maxHP;
        hp = data.maxHP;
    }

    public void Initialize(HealthData data)
    {
        maxHP = data.maxHP;
        hp = data.maxHP;
    }
}

