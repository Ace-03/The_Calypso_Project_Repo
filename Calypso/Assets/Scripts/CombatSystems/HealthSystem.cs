using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHP;
    public int hp;
    public int bonusHP;
    public bool invulnerable;
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
        {
            DEBUG_Change_Mat();
        }
        if (invulnerable) { return; }

        if (bonusHP > 0)
        {
            bonusHP -= (int)info.damage;
        }
        if (bonusHP <= 0)
        {
            bonusHP = 0;
            hp -= (int)info.damage;
        }
        if (hp <= 0) {
            Die();
        }
    }
    public void Die()
    {
        if(DEBUG_RESPAWN)
        {
            FindAnyObjectByType<Spawner>().Spawn();
        }
        Destroy(this.gameObject);
    }
    public void DEBUG_Change_Mat()
    {
        mr = GetComponent<MeshRenderer>();
        float percentage = (float)hp/(float)maxHP;
        Color interpolatedColor = DEBUG_Gradient.Evaluate(percentage);
        mr.material.color = interpolatedColor;
    }
}
