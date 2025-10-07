using System.Collections;
using UnityEngine;

public class VisualEffectsHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Transform targetTransform;
    private GenericHealth health;

    [SerializeField]
    private bool autoInitialize = false;

    private bool isFlashing = false;
    private bool isShaking = false;

    private void Awake()
    {
        if (autoInitialize)
            Initialize();
    }

    public void Initialize(SpriteRenderer sr = null, GenericHealth health = null)
    {
        if (sr != null)
        {
            spriteRenderer = sr;
        }
        else if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
        {
            Debug.LogWarning("VisualEffectHandler: No SpriteRenderer found on " + gameObject.name);
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            targetTransform = spriteRenderer.transform;
        }
        else
        {
            Debug.LogWarning("VisualEffectHandler: No SpriteRenderer found on " + gameObject.name);
            originalColor = Color.white;
        }

        if (health == null && !TryGetComponent<GenericHealth>(out this.health))
        {
            Debug.LogWarning("VisualEffectHandler: No Health component found on " + gameObject.name);
        }
    }

    public void TriggerVisualEffects(DamageInfo damgeInfo)
    {
        TriggerFlash((int)damgeInfo.damage);
        TriggerShake((int)damgeInfo.damage);
    }

    private void TriggerFlash(int damage)
    {
        if (!isFlashing)
        {
            StartCoroutine(HitFlash(damage));
        }
    }

    private void TriggerShake(int damage)
    {
        if (!isShaking)
        {
            StartCoroutine(HitShake(damage));
        }
    }

    private IEnumerator HitFlash(int damage)
    {
        isFlashing = true;

        float flashDuration = 0.1f + ((damage / health.maxHP) * 0.2f);
        float elapsed = 0.0f;

        while (elapsed < flashDuration)
        {
            spriteRenderer.color = Color.red;
            elapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        spriteRenderer.color = originalColor;
    }

    private IEnumerator HitShake(int damage)
    {
        isShaking = true;

        float shakeMagnitude = Vector3.Distance(Camera.main.transform.position, transform.position) * 0.05f + (damage / health.maxHP);
        float shakeDuration = 0.1f + (damage / health.maxHP);

        Vector3 originalLocalPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            targetTransform.localPosition = originalLocalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = originalLocalPosition;
        isShaking = false;
    }
}

