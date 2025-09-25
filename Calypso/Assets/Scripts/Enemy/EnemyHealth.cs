using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealthSystem
{
    public int maxHP;
    public int hp;
    public bool DEBUG_RESPAWN;
    public bool DEBUG_MATS;
    public Gradient DEBUG_Gradient;
    private MeshRenderer mr;

    private void Start()
    {
        if (DEBUG_MATS)
        {
            DEBUG_Change_Mat();
        }
    }

    public void TakeDamage(DamageInfo info)
    {
        if (DEBUG_MATS)
            DEBUG_Change_Mat();
        
        hp -= (int)info.damage;
        
        if (hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    public void Die()
    {
        GetComponent<EnemyInitializer>().OnDeath();
    }
    
    public void DEBUG_Change_Mat()
    {
        mr = GetComponent<MeshRenderer>();
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
