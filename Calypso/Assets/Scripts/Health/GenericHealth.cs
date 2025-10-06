using System.Collections;
using UnityEngine;

public class GenericHealth : MonoBehaviour, IHealthSystem
{
    [SerializeField]
    public int maxHP;
    public int hp;

    private bool isFlashing = false;
    private bool isShaking = false;

    private void Awake()
    {
        Initialize(new HealthData { maxHP = maxHP });
    }

    public void TakeDamage(DamageInfo info)
    {
        TakeDamageRaw((int)info.damage);
    }

    public void TakeDamageRaw(int damage)
    {
        hp -= (int)damage;
        
        if (hp <= 0)
            Die();
    }
    
    public void Die()
    {
        hp = maxHP;
        Destroy(gameObject);
    }
    
    public int GetMaxHealth()
    {
        return maxHP;
    }

    public void Initialize(HealthData data)
    {
        maxHP = data.maxHP;
        hp = maxHP;
    }

    private void DamageVisualEffect()
    {

    }

    private void TriggerFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(HitFlash());
        }
    }

    private void TriggerShake()
    {
        if (!isShaking)
        {
            StartCoroutine(HitShake());
        }
    }

    private IEnumerator HitFlash()
    {
        isFlashing = true;
    }

    private IEnumerator HitShake(int damage)
    {
        isShaking = true;

        float shakeMagnitude = Vector3.Distance(Camera.main.transform.position, transform.position) * 0.05f + (damage / maxHP);
        float shakeDuration = 0.1f + (damage / maxHP);

        Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalLocalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = originalLocalPosition;
        isShaking = false;
    }
}
