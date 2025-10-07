using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHP;
    public int hp;

    private VisualEffectsHandler vfxHandler;

    private void Awake()
    {
        Initialize(new HealthData { maxHP = maxHP });
    }

    public virtual void TakeDamage(DamageInfo info)
    {
        if (vfxHandler != null)
        {
            vfxHandler.TriggerVisualEffects(info);
        }
        TakeDamageRaw((int)info.damage);
    }

    public virtual void TakeDamageRaw(int damage)
    {
        hp -= (int)damage;

        if (hp <= 0)
            Die();
    }

    public virtual void Die()
    {
        hp = maxHP;
        Destroy(gameObject);
    }

    public void Initialize(HealthData data, VisualEffectsHandler vfx = null)
    {
        maxHP = data.maxHP;
        hp = maxHP;

        if (vfx == null && !TryGetComponent<VisualEffectsHandler>(out vfxHandler))
        {
            Debug.LogWarning("No VisualEffectsHandler found on GenericHealth object. Visual effects will be disabled.");
        }
        else
        {
            vfxHandler = vfx;
        }
    }
}