using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    private ParticleSystem _system;
    private void OnTriggerEnter(Collider other)
    {
        if (!TryGetComponent<ParticleSystem>(out _system))
            _system = GetComponentInChildren<ParticleSystem>();

        if (_system == null)
        {
            Debug.LogError("No partice system found on particle trigger object");
            return;
        }

        if (!_system.isPlaying)
            _system.Play();
    }
}
