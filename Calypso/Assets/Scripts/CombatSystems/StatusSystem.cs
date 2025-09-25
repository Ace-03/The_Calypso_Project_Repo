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
    private HealthSystem healthSystem;
    private AI_NAV ai;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        ai = GetComponent<AI_NAV>();
    }
    private void Update()
    {
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
            if(ai.speed == 0)
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
    }
    private void UpdateStacks()
    {
        if (stunTimer > maxStatTime && stunStacks < maxStatStacks)
        {
            stunStacks = Mathf.Clamp(stunStacks + 1, 0, MAX_STUN_STACKS);
            stunTimer = Mathf.Clamp(stunTimer - maxStatTime, 1f, maxStatTime);
        }
        if (poisonTimer > maxStatTime && poisonStacks < maxStatStacks)
        {
            poisonStacks = Mathf.Clamp(poisonStacks + 1, 0, maxStatStacks);
            poisonTimer = Mathf.Clamp(poisonTimer - maxStatTime, 1f, maxStatTime);
        }
        if(slowdownTimer > maxStatTime && slowdownStacks < maxStatStacks)
        {
            slowdownStacks = Mathf.Clamp(slowdownStacks + 1, 0, maxStatStacks);
            slowdownTimer = Mathf.Clamp(slowdownTimer - maxStatTime, 1f, maxStatTime);
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
    private void DoStun()
    {
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
        ai.speed = maxSpeed - (maxSpeed * 0.1f * slowdownStacks);
    }
    public void DoKnockback()
    {
        //Implement here
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
