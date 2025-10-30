using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
public class Explosion : MonoBehaviour
{
    float dieTimer = 1f;
    float startDieTime = 1f;
    float desiredScale = 1f;
    public BulletTrigger trigger;
    public Collider col;
    public void Setup(float radius, float t, BulletTrigger trig)
    {
        dieTimer = t;
        startDieTime = t;
        desiredScale = radius;
        trigger = trig;
        col.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(trigger != null)
        {
            trigger.TryExternalDamage(other.gameObject);
        }
        else
        {
            Debug.LogError("Bullet Trigger Not Active");
        }
    }

    private void Update()
    {
        dieTimer -= Time.deltaTime;
        float currentLifetimePercent = 1f - dieTimer/startDieTime;
        transform.localScale = Vector3.one * desiredScale * currentLifetimePercent;

        if (dieTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
