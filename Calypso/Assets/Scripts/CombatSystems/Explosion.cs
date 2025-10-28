using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(BulletTrigger))]
public class Explosion : MonoBehaviour
{
    float dieTimer = 1;
    public void Setup(float radius, float t)
    {
        dieTimer = radius;
        transform.localScale = Vector3.one * radius;
    }

    private void Update()
    {
        dieTimer -= Time.deltaTime;
        if (dieTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
