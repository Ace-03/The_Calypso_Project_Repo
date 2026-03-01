using UnityEngine;

public class BaseHealth : GenericHealth
{
    private void Start()
    {
        Initialize(new HealthData { maxHP = maxHP });
    }

    public override void Initialize(HealthData data, VisualEffectsHandler vfx = null)
    {
        base.Initialize(data, vfx);

        HudManager.Instance?.health?.UpdateBaseHealth(hp, maxHP);
    }

    public override void TakeDamageRaw(int damage)
    {
        base.TakeDamageRaw(damage);

        HudManager.Instance.health.UpdateBaseHealth(hp, maxHP);
    }

    public bool projectBaseDamage(int damage)
    {
        return hp - damage <= 0;
    }
}
