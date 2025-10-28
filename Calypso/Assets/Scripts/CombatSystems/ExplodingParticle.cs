using UnityEngine;

public class ExplodingParticle : MonoBehaviour
{
    ParticleSystem ps;
    public GameObject explosionPrefab;
    public float radius;
    public float time;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    // Jetsune Code, but stripped down because I'm not making a pool. (Feel free to change)
    void OnParticleCollision(GameObject other)
    {
        // Get collision point(s)
        ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[10];
        int collisionCount = ps.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < collisionCount; i++)
        {
            Vector3 collisionPoint = collisionEvents[i].intersection;
            Explode(collisionPoint);
        }

    }
    //Spawn explosion at point
    void Explode(Vector3 position)
    {
        GameObject explosion;

        explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.GetComponent<Explosion>().Setup(radius, time);
    }
}
