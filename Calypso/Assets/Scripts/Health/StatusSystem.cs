using UnityEngine;

public class StatusSystem : MonoBehaviour
{
    public float maxStatTime = 10f;
    public int maxStatStacks = 3;
    private int MAX_STUN_STACKS = 3;
    private float stunTimer;
    private int stunStacks;
    private float poisonTimer;
    private int poisonStacks;
    private float slowdownTimer;
    private int slowdownStacks;
    private float poisonTickRate = 2f;
    private float poisonTickTimer;
    private float knockbackTimer;
    private int knockbackStacks;
    private float knockbackStrength;
    private GenericHealth healthSystem;
    private AI_NAV ai;

    private void Start()
    {
        healthSystem = GetComponent<GenericHealth>();
        ai = GetComponent<AI_NAV>();
    }
    private void Update()
    {
        UpdateTimers();
        ApplyStats();
    }
    public void ResetTimers()
    {
        stunTimer = 0f;
        poisonTimer = 0f;
        slowdownTimer = 0f;
        knockbackStrength = 0f;
        knockbackTimer = 0f;
        UpdateTimers();
        ApplyStats();
    }
    private void ApplyStats()
    {
        if (stunTimer > 0)
        {
            DoStun();
        }
        else if(ai != null)
        {
            if(ai.speed <= 0 && knockbackTimer <= 0)
            {
                ai.ResetSpeed();
            }
        }
        if (knockbackTimer > 0)
        {
            DoKnockback();
        }
        else
        {
            if (ai.speed <= 0 && stunTimer <=0)
            {
                ai.ResetSpeed();
            }
        }
        if (poisonTimer > 0)
        {
            DoPoison();
        }
        if (slowdownTimer > 0)
        {
            DoSlowdown();
        }
    }
    private void UpdateTimers()
    {
        stunTimer = Mathf.Clamp(stunTimer - Time.deltaTime, 0, maxStatTime);
        poisonTimer = Mathf.Clamp(poisonTimer - Time.deltaTime, 0, maxStatTime);
        slowdownTimer = Mathf.Clamp(slowdownTimer-Time.deltaTime, 0, maxStatTime);
        knockbackTimer = Mathf.Clamp(knockbackTimer - Time.deltaTime, 0, maxStatTime);
        if(stunTimer == 0)
        {
            maxStatStacks -= stunStacks;
            stunStacks = 0;
        }
        if(poisonTimer == 0)
        {
            poisonStacks = 0;
        }
        if(slowdownTimer == 0)
        {
            slowdownStacks = 0;
        }
        if (knockbackTimer == 0)
        {
            knockbackStacks = 0;
        }
    }
    private void UpdateStacks()
    {
        while (stunTimer > maxStatTime && stunStacks < maxStatStacks)
        {
            stunStacks = Mathf.Clamp(stunStacks + 1, 0, MAX_STUN_STACKS);
            stunTimer = Mathf.Clamp(stunTimer - maxStatTime, 2f, maxStatTime);
        }
        while (poisonTimer > maxStatTime && poisonStacks < maxStatStacks)
        {
            poisonStacks = Mathf.Clamp(poisonStacks + 1, 0, maxStatStacks);
            poisonTimer = Mathf.Clamp(poisonTimer - maxStatTime, 2f, maxStatTime);
        }
        while (slowdownTimer > maxStatTime && slowdownStacks < maxStatStacks)
        {
            slowdownStacks = Mathf.Clamp(slowdownStacks + 1, 0, maxStatStacks);
            slowdownTimer = Mathf.Clamp(slowdownTimer - maxStatTime, 2f, maxStatTime);
        }
        while (knockbackTimer > maxStatTime && knockbackStacks < maxStatStacks)
        {
            knockbackStacks = Mathf.Clamp(knockbackStacks + 1, 0, maxStatStacks);
            knockbackTimer = Mathf.Clamp(knockbackTimer - maxStatTime, 2f, maxStatTime);
        }
    }
    public void ApplyStun(float time)
    {
        stunTimer += time;
        UpdateStacks();
    }
    public void ApplyPoison(float time)
    {
        poisonTimer += time;
        UpdateStacks();
    }
    public void ApplySlowdown(float time)
    {
        slowdownTimer += time;
        UpdateStacks();
    }
    public void ApplyKnockback(float strength)
    {
        knockbackTimer = 0.2f;
        knockbackStrength = strength;
        UpdateStacks();
    }
    private void DoStun()
    {
        if (knockbackTimer > 0)
        {
            return;
        }
        if (ai == null)
        {
            Debug.LogError("Couldn't find AI_NAV module");
            return;
        }
        ai.speed = 0;
    }
    private void DoPoison()
    {
        if(poisonTickTimer > 0)
        {
            poisonTickTimer -= Time.deltaTime;
        }
        else
        {
            poisonTickTimer = 1f / poisonTickRate;
            int poisonDamage = (int)(healthSystem.maxHP * 0.1f * (poisonStacks + 1));
            if (poisonDamage < 1) { poisonDamage = poisonStacks; }
            if (poisonDamage < 1) { poisonDamage = 1; }
            healthSystem.TakeDamageRaw(poisonDamage);
        }
    }
    private void DoSlowdown()
    {
        if (ai == null)
        {
            Debug.LogError("Couldn't find AI_NAV module");
            return;
        }
        if (stunStacks > 0 || ai.speed == 0)
        {
            return;
        }
        float maxSpeed = ai.GetMaxSpeed();
        ai.speed = maxSpeed - (maxSpeed * 0.2f * slowdownStacks);
    }
    public void DoKnockback()
    {
        if(ai == null)
        {
            return;
        }
        ai.speed = 0;
        //Over-Simplfied implementation
        Vector3 dir = (transform.position - ai.target.position).normalized;
        transform.position += dir * knockbackStrength * (knockbackStacks + 1) * Time.deltaTime;
    }

    public bool IsStunned()
    {
        return stunTimer > 0f;
    }
    public bool IsPoisoned()
    {
        return poisonTimer > 0f;
    }
    public bool IsSlowed()
    {
        return slowdownTimer > 0f;
    }
}
