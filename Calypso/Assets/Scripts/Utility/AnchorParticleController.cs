using UnityEngine;

public class AnchorParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem LandingBurst;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Event occured on anchor");
        if (collision != null)
        {
            Debug.Log($"Object anchor hit is {collision.gameObject.name}");
            if (LandingBurst != null && !LandingBurst.isPlaying)
            {
                LandingBurst.Play();
            }
        }
    }
}
