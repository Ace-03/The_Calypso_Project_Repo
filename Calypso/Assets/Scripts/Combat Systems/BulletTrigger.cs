using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    public int bulletDamage = 1;
    void OnParticleCollision(GameObject other)
    {
        TryDamage(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other.gameObject);
    }

    private void TryDamage(GameObject other)
    {
        if (other.GetComponent<HealthSystem>() != null)
        {
            other.GetComponent<HealthSystem>().TakeDamage(bulletDamage);
        }
    }
}
