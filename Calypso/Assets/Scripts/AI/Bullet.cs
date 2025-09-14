using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 1;
    void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<HealthSystem>() != null)
        {
            other.GetComponent<HealthSystem>().TakeDamage(bulletDamage);
        }
    }

}
