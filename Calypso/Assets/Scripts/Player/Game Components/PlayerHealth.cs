using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [Header("Events")]
    [SerializeField] private OnRestoreHealthEventSO healEvent;
    private int bonusHP;

    PlayerManager playerManager;

    private void OnEnable()
    {
        healEvent.RegisterListener(HealThroughEvent);
    }

    private void OnDisable()
    {
        healEvent.UnregisterListener(HealThroughEvent);

    }

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

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

        playerManager.OnDeath();
        base.Die();
    }

    private void HealThroughEvent(HealPayload payload)
    {
        heal((int)payload.value);
    }

    public void heal(int amount)
    {
        hp += amount;
        if (hp > maxHP + bonusHP)
            hp = maxHP + bonusHP;

        HudManager.Instance.health.UpdatePlayerHealth(hp, maxHP);
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
        HudManager.Instance.health.UpdatePlayerHealth(hp, maxHP);
    }

    public void UpdateHealthStats(StatSystem statSystem)
    {
        int newMaxHP = (int)statSystem.GetFinalValue(StatType.MaxHealth);
        int hpDiff = newMaxHP - maxHP;

        maxHP = newMaxHP;
        hp += hpDiff;
        invulnerabilityDuration = statSystem.GetFinalValue(StatType.Invulnerability);

        HudManager.Instance.health.UpdatePlayerHealth(hp, maxHP);
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

    public bool GetInvulnerable() => invulnerable;
}

