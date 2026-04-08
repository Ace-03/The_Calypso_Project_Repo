using UnityEngine;

public class EnemyHealth : GenericHealth
{
    public bool DEBUG_RESPAWN;
    public bool DEBUG_MATS;
    public Gradient DEBUG_Gradient;
    public MeshRenderer mr;
    private StatusSystem statusSystem;

    private void Start()
    {
        if (DEBUG_MATS)
        {
            DEBUG_Change_Mat();
        }
        statusSystem = GetComponent<StatusSystem>();
    }

    public override void TakeDamage(DamageInfo info)
    {
        if (invulnerable) { return; }

        EnemyDefinitionSO enemy = GetComponent<EnemyInitializer>().GetEnemyData();

        if (statusSystem != null)
        {
            statusSystem.ApplyPoison(Mathf.Max(info.poisonDuration - enemy.poisonResistance, 0));
            statusSystem.ApplySlowdown(Mathf.Max(info.stunDuration - enemy.slowResistance, 0));
            statusSystem.ApplyStun(Mathf.Max(info.stunDuration - enemy.stunResistance, 0));
            statusSystem.ApplyKnockback(Mathf.Max(info.knockbackStrength - enemy.knockbackResistance, 0));
        }

        base.TakeDamage(info);
    }

    public override void TakeDamageRaw(float damage)
    {

        if (DEBUG_MATS)
            DEBUG_Change_Mat();

        base.TakeDamageRaw(damage);
    }
    
    public override void Die()
    {
        if (statusSystem != null)
            statusSystem.ResetTimers();

        base.Die();
        GetComponent<EnemyInitializer>().OnDeath();
    }
    
    public void DEBUG_Change_Mat()
    {
        if (mr == null)
            mr = GetComponent<MeshRenderer>();
        if(mr == null)
            return;

        float percentage = hp/maxHP;
        Color interpolatedColor = DEBUG_Gradient.Evaluate(percentage);
        mr.material.color = interpolatedColor;
    }
}
