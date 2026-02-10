using Unity.VisualScripting;
using UnityEngine;

public class PoolSizeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private float spriteSizeModifier;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private Transform targetTransform;
    private Animator animator;

    private float targetDuration;
    private float maxSize;
    private float currentRadius;
    private float timer;
    private float startRadius;
    private bool initialized;

    void Awake()
    {
        startRadius = targetTransform.localScale.magnitude;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        UpdateTimer();
        ChangeRadius();
        ChangeScale(currentRadius);
    }

    void UpdateTimer()
    {
        if (timer < targetDuration && initialized)
            timer += Time.deltaTime;
        if (timer >= targetDuration)
        {
            Invoke(nameof(DisableTrigger), 0.5f);
            Destroy(gameObject);
        }
    }

    void ChangeRadius() => 
        currentRadius = startRadius + Mathf.PingPong(timer, targetDuration) * maxSize;

    void ChangeScale(float radius) =>
        targetTransform.localScale = new Vector3(radius, radius, radius);


    public void InitializePool(float duration, float targetSize)
    {
        maxSize = targetSize;
        targetDuration = duration;
        initialized = true;

        SetAnimationDuration(duration);
        spriteTransform.localScale *= targetSize;
    }

    private void SetAnimationDuration(float duration)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float clipLength = stateInfo.length;

        animator.speed = clipLength / duration;
    }

    private void DisableTrigger() =>
        GetComponentInChildren<Collider>().enabled = false;
}
