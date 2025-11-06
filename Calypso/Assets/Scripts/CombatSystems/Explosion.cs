using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
public class Explosion : MonoBehaviour
{
    float dieTimer = 1f;
    float startDieTime = 1f;
    float desiredScale = 1f;
    int damage = 100;
    public BulletTrigger trigger;
    public SphereCollider col;
    public void Setup(float radius, float t, BulletTrigger trig)
    {
        dieTimer = t;
        startDieTime = t;
        desiredScale = radius;
        trigger = trig;
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyStatusComponent = other.GetComponent<EnemyHealth>();
            enemyStatusComponent.TakeDamageRaw(damage);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyStatusComponent = other.GetComponent<EnemyHealth>();
            enemyStatusComponent.TakeDamageRaw(damage);
        }
    }

    private void Update()
    {
        dieTimer -= Time.deltaTime;
        float currentLifetimePercent = 1f - dieTimer/startDieTime;
        transform.localScale = Vector3.one * desiredScale;
        col.radius = desiredScale;

        if (dieTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
