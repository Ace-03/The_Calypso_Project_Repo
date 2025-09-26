using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealthSystem
{
    public int maxHP;
    public int hp;
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

    public void TakeDamage(DamageInfo info)
    {

        if (statusSystem != null)
        {
            statusSystem.ApplyPoison(info.poisonDuration);
            statusSystem.ApplySlowdown(info.stunDuration);
            statusSystem.ApplyStun(info.stunDuration);
            statusSystem.ApplyKnockback(info.knockbackStrength);
        }

        if (DEBUG_MATS)
            DEBUG_Change_Mat();
        
        hp -= (int)info.damage;
        
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    public void TakeDamageRaw(int damage)
    {
        Debug.Log("Raw Damage Taken: " + damage);

        if (DEBUG_MATS)
            DEBUG_Change_Mat();

        hp -= (int)damage;
        
        if (hp <= 0)
            Die();
    }
    
    public void Die()
    {
        if (statusSystem != null)
        {
            statusSystem.ResetTimers();
        }
        hp = maxHP;
        GetComponent<EnemyController>().OnDeath();
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

    public void Initialize(HealthData data)
    {
        maxHP = data.maxHP;
        hp = data.maxHP;
    }
}
