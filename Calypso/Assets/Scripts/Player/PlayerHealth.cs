using UnityEngine;

public class PlayerHealth : GenericHealth
{
    private int bonusHP;
    private bool invulnerable;

    private float invulnerabilityDuration = 0.5f;
    private float invulnerabilityTimer;

    private void Update()
    {
        if (!invulnerable) { return; }

        invulnerabilityTimer -= Time.deltaTime;

        if (invulnerabilityTimer <= 0)
            invulnerable = false;
    }

    public override void TakeDamage(DamageInfo info)
    {
        if (invulnerable) { return; }

        vfxHandler.TriggerInvulnerabilityEffect(invulnerable, invulnerabilityDuration);
        base.TakeDamage(info);
    }

    public override void Die()
    {
        // Game Over Logic Here

        // stubbed out for now
        Debug.Log("Player Died");
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(Vector3.up * 100, ForceMode.Impulse);
        }

        base.Die();
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
        invulnerabilityDuration = data.invulnerabilityDuration;
        maxHP = data.maxHP;
        hp = data.maxHP;
    }

    public override void TakeDamageRaw(int damage)
    {
        Debug.Log("Damage Taken by player: " + damage);
        if (bonusHP > 0)
        {
            int damageToBonus = Mathf.Min(bonusHP, damage);
            bonusHP -= damageToBonus;
            damage -= damageToBonus;
        }

        hp -= (int)Mathf.Clamp(damage, 0f, maxHP);
        invulnerabilityTimer = invulnerabilityDuration;
        invulnerable = true;

        if (hp <= 0)
            Die();
    }
}

