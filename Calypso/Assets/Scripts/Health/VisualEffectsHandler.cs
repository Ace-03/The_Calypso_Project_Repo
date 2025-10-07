using System.Collections;
using UnityEngine;

public class VisualEffectsHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Transform targetTransform;
    private float maxHP;

    [SerializeField]
    private bool autoInitialize = false;

    private bool isFlashing = false;
    private bool isShaking = false;

    private void Awake()
    {
        if (autoInitialize)
            Initialize();
    }

    public void Initialize(SpriteRenderer sr = null, float hp = 0)
    {
        if (sr != null)
        {
            spriteRenderer = sr;
        }
        else if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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

        if (hp == 0)
        {
            TryGetHP();
        }
        else
        {
            maxHP = hp;
        }
    }

    public void TriggerHitEffect(DamageInfo damgeInfo)
    {
        TriggerFlash((int)damgeInfo.damage);
        TriggerShake((int)damgeInfo.damage);
    }

    public void TriggerInvulnerabilityEffect(bool isInvulnerable, float duration)
    {
        TriggerInvulnerabilityFlash(isInvulnerable, duration);
    }

    private void TriggerInvulnerabilityFlash(bool isInvulnerable, float duration)
    {
        if (!isInvulnerable)
        {
            StartCoroutine(InvulnerabilityFlash(duration));
        }
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

    private IEnumerator InvulnerabilityFlash(float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Color currentColor = spriteRenderer.color;

            float cosVal = Mathf.Cos(Time.time * 10);
            float alpha = (cosVal * 0.25f) + 0.75f;
            
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            yield return new WaitForFixedUpdate();
        }
        spriteRenderer.color = originalColor;
    }


    private IEnumerator HitFlash(int damage)
    {
        isFlashing = true;
        CheckForHP();

        float flashDuration = 0.06f + ((damage / maxHP) * 0.05f);
        float elapsed = 0.0f;

        while (elapsed < flashDuration)
        {
            spriteRenderer.color = Color.red;
            elapsed += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }

    private IEnumerator HitShake(int damage)
    {
        isShaking = true;
        CheckForHP();
        
        float shakeMagnitude = Vector3.Distance(Camera.main.transform.position, transform.position) * 0.001f + (damage / maxHP) * 0.01f;
        float shakeDuration = 0.1f + (damage / maxHP) * 0.05f;

        Vector3 originalLocalPosition = targetTransform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            targetTransform.localPosition = originalLocalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        targetTransform.localPosition = originalLocalPosition;
        isShaking = false;
    }

    private void CheckForHP()
    {
        if (maxHP == 0)
        {
            Debug.LogWarning($"{this.name}: health is 0, tyring to get health");
            TryGetHP();
        }
    }

    private void TryGetHP()
    {
        if (GetComponent<GenericHealth>() == null)
        {
            Debug.LogWarning("VisualEffectHandler: No Health given to " + gameObject.name);
        }
        else
        {
            maxHP = GetComponent<GenericHealth>().maxHP;
        }
    }
}

