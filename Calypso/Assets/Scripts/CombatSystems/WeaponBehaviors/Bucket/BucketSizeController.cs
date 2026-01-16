using Unity.VisualScripting;
using UnityEngine;

public class BucketSizeController : MonoBehaviour, IWeaponBehavior
{
    public float maxRadius = 4f;
    public float minRadius = 2f;
    float currentRadius;
    public float switchCycleTime = 4f;
    public Transform objectTransform;
    public BulletTrigger bulletTrigger;
    float timer;
    float normalizedTimerValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRadius = minRadius;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        ChangeRadius();
        ChangeScale(currentRadius);
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
        maxRadius = weapon.GetArea();
        minRadius = weapon.GetArea()/2;
        switchCycleTime = weapon.GetDuration();
        bulletTrigger.SetDamageSource(weapon.GetDamageSource());
    }

    public bool IsAimable()
    {
        return false;
    }
}
