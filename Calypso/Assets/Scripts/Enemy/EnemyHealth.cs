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

        if (statusSystem != null)
        {
            statusSystem.ApplyPoison(info.poisonDuration);
            statusSystem.ApplySlowdown(info.stunDuration);
            statusSystem.ApplyStun(info.stunDuration);
            statusSystem.ApplyKnockback(info.knockbackStrength);
        }

        base.TakeDamage(info);
    }

    public override void TakeDamageRaw(int damage)
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
        {
            mr = GetComponent<MeshRenderer>();
        }
        if(mr == null)
        {
            return;
        }
        float percentage = (float)hp/(float)maxHP;
        Color interpolatedColor = DEBUG_Gradient.Evaluate(percentage);
        mr.material.color = interpolatedColor;
    }
}
