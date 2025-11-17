using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHP;
    public int hp;
    public VisualEffectsHandler vfxHandler;

    protected bool invulnerable;

    protected float invulnerabilityDuration = 0.1f;
    protected float invulnerabilityTimer;

    private bool dead;

    private void Awake()
    {
        Initialize(new HealthData { maxHP = maxHP });
    }


    private void Update()
    {
        if (!invulnerable) { return; }

        invulnerabilityTimer -= Time.deltaTime;

        if (invulnerabilityTimer <= 0)
            invulnerable = false;
    }


    public virtual void TakeDamage(DamageInfo info)
    {
        if (invulnerable) { return; }

        TakeDamageRaw((int)info.damage);

        if (!dead && vfxHandler != null)
        {
            vfxHandler.TriggerHitEffect(info);
        }
    }

    public virtual void TakeDamageRaw(int damage)
    {
        hp -= damage;

        invulnerabilityTimer = invulnerabilityDuration;
        invulnerable = true;

        if (hp <= 0)
            Die();
    }

    public virtual void Die()
    {
        hp = maxHP;
        dead = true;
    }

    public void Initialize(HealthData data, VisualEffectsHandler vfx = null)
    {
        maxHP = data.maxHP;
        hp = maxHP;
        dead = false;
        if (vfx != null)
        {
            Debug.Log("VFX IS: " + vfx);
            vfxHandler = vfx;
        }
        else if (!TryGetComponent<VisualEffectsHandler>(out vfxHandler))
        {
            Debug.LogWarning("No VisualEffectsHandler found on GenericHealth object. Visual effects will be disabled.");
        }
    }
}