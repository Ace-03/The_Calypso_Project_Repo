using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttraction : MonoBehaviour, IWeaponBehavior
{
    public Transform Target;

    private ParticleSystem system;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

    private float timer;
    private float lerpTime = 2f;

    [SerializeField] private float drag = 5f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float waitingTimeBeforeApplyingForce = 0.3f;
    private void Start()
    {
        Target = GameObject.FindWithTag("Target").transform;
    }

    void Update()
    {
        if (system == null)
        {
            system = GetComponent<ParticleSystem>();
        }

        timer += Time.deltaTime;
        if (timer > lerpTime)
        {
            system.Emit(50);
            timer = 0f;
        }

        if (timer < waitingTimeBeforeApplyingForce)
            return;


        var count = system.GetParticles(particles);
        for (int i = 0; i < count; i++)
        {
            var particle = particles[i];

            var directionTowardsTarget = (Target.position - particle.position).normalized;
            particle.velocity = particle.velocity + directionTowardsTarget * Time.deltaTime * gravity;
            particle.velocity = particle.velocity * (1 - Time.deltaTime * drag);
            particles[i] = particle;
        }

        system.SetParticles(particles, count);
    }

    public void Attack(WeaponController weapon)
    {
        Debug.Log("Running attack in weapon Controller");
        system.Play();
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        return;
    }

    public bool IsAimable()
    {
        return true;
    }
}