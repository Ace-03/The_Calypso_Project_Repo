using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHP;
    public int hp;
    public VisualEffectsHandler vfxHandler;

    private bool dead;

    private void Awake()
    {
        Initialize(new HealthData { maxHP = maxHP });
    }

    public virtual void TakeDamage(DamageInfo info)
    {

        TakeDamageRaw((int)info.damage);

        if (!dead && vfxHandler != null)
        {
            vfxHandler.TriggerHitEffect(info);
        }
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