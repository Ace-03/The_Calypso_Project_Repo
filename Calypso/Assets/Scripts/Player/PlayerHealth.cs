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
        Debug.Log("Player Died");

        PlayerManager.Instance.OnDeath();
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

        HudManager.Instance.health.UpdatePlayerHealth(hp, maxHP);

        if (hp <= 0)
            Die();
    }
}

