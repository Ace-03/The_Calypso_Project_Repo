using Unity.VisualScripting;
using UnityEngine;

public class BucketSizeController : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private float maxRadiusMult;
    [SerializeField] private float minRadiusDivisor;
    [SerializeField] private Transform spriteTransform;
    private float maxRadius = 4f;
    private float minRadius = 2f;
    private float currentRadius;
    private float switchCycleTime = 4f;
    public Transform objectTransform;
    public BulletTrigger bulletTrigger;
    float timer;
    float normalizedTimerValue;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentRadius = minRadius;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        ChangeRadius();
        ChangeScale(currentRadius);
        spriteTransform.eulerAngles = new Vector3(90, 0, 0);
    }

    void ChangeRadius()
    {
        currentRadius = minRadius + Mathf.PingPong(normalizedTimerValue, 1) * (maxRadius - minRadius);
    }
    void ChangeScale(float radius)
    {
        objectTransform.localScale = new Vector3(radius, radius, radius);
    }
    void UpdateTimer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = switchCycleTime;
        }
        normalizedTimerValue = (timer / switchCycleTime) * 2;
    }

    public void Attack(WeaponController weapon)
    {

    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        maxRadius = weapon.GetArea()*maxRadiusMult;
        minRadius = weapon.GetArea()/minRadiusDivisor;
        switchCycleTime = weapon.GetDuration();
        bulletTrigger.SetDamageSource(weapon.GetDamageSource());
        SetAnimationDuration(weapon.GetDuration());
        spriteTransform.localScale = Vector3.one * weapon.GetArea() * 2;
    }

    private void SetAnimationDuration(float duration)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float clipLength = stateInfo.length;

        animator.speed = clipLength / duration;
    }

    public bool IsAimable()
    {
        return false;
    }
}
