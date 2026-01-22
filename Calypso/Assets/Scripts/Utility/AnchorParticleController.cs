using UnityEngine;

public class AnchorParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem LandingBurst;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (LandingBurst != null && !LandingBurst.isPlaying)
            {
                LandingBurst.Play();

            }
        }
    }
}
