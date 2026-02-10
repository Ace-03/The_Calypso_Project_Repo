using Unity.VisualScripting;
using UnityEngine;

public class PoolSizeController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private float TargetSizeModifier;
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private Transform targetTransform;
    public BulletTrigger bulletTrigger;
    private Animator animator;

    private float maxSize = 4f;
    private float currentRadius;

    private float timer;
    private float normalizedTimerValue;
    private float startRadius;
    private bool initialized;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startRadius = targetTransform.localScale.magnitude;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        ChangeRadius();
        ChangeScale(currentRadius);
    }

    void UpdateTimer()
    {
        if (timer > 0 && initialized)
            timer -= Time.deltaTime;
        if (timer <= 0)
            Invoke(nameof(DisableTrigger), 0.5f);
    }

    void ChangeRadius()
    {

        currentRadius = startRadius + Mathf.PingPong(timer, 1) * maxSize;
    }
    void ChangeScale(float radius)
    {
        targetTransform.localScale = new Vector3(radius, radius, radius);
    }


    public void InitializePool(float duration, float targetSize)
    {
        maxSize = targetSize;
        timer = duration;
        initialized = true;

        SetAnimationDuration(duration);
        spriteTransform.localScale = Vector3.one * targetSize * 2;
    }

    private void SetAnimationDuration(float duration)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float clipLength = stateInfo.length;

        animator.speed = clipLength / duration;
    }

    private void DisableTrigger()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }
}
