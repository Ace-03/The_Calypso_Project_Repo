using NUnit;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class PierceHandler : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ParticleWeaponBase weaponBehavior;
    public float detectionRadius = 1f;
    public LayerMask layerMask;
    public int maxCollisionTicks = 2;

    private ParticleSystem.Particle[] particles;
    WeaponDefinitionSO weaponData;
    EnemyDefinitionSO enemyData;

    private Dictionary<uint, int> particleHitCounts = new Dictionary<uint, int>();

    private void Awake()
    {
        weaponData = GetComponentInParent<WeaponController>()?.GetWeaponData();
        enemyData = GetComponentInParent<EnemyInitializer>()?.GetEnemyData();
    }

    void Start()
    {
        if (particleSystem == null)
            particleSystem = GetComponent<ParticleSystem>();

        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        layerMask = LayerMask.GetMask("Enemy");
    }
    private float timePerTick = 0.1f;
    private float currentTime;
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timePerTick)
        {
            currentTime = 0f;
            CheckCollision();
        }
    }
    void CheckCollision()
    {
        int particleCount = particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            var particle = particles[i];
            Vector3 particlePosition = particles[i].position;

            if (particleSystem.main.simulationSpace == ParticleSystemSimulationSpace.Local)
                particlePosition = particleSystem.transform.TransformPoint(particlePosition);

            Collider[] hits = Physics.OverlapSphere(particlePosition, detectionRadius, layerMask);

            if (hits.Length > 0)
            {

                // Use randomSeed as a unique key for the particle
                uint id = particle.randomSeed;

                if (!particleHitCounts.ContainsKey(id))
                    particleHitCounts[id] = 0;
                particleHitCounts[id] += hits.Length;

                foreach (Collider hit in hits)
                {
                    GameObject other = hit.gameObject;
                    if (other.GetComponent<GenericHealth>() != null)
                    {
                        DamageInfo damageInfo = new DamageInfo();

                        if (other.CompareTag("Enemy"))
                        {
                            Debug.Log($"Particle {id} hit {hit.gameObject.name}, total hits: {particleHitCounts[id]}");
                            damageInfo = DamageCalculator.GetDamageFromPlayer(weaponData);
                            other.GetComponent<GenericHealth>().TakeDamage(damageInfo);
                            CheckNumberHits(i,id);
                        }
                    }
                }
            }
        }
    }
    void CheckNumberHits(int index, uint id)
    {
        if (particleHitCounts[id] >= maxCollisionTicks)
        {
            particles[index].remainingLifetime = 0f;

            Debug.Log($"Particle {id} died via HITS");

            particleSystem.Clear();
            particleSystem.SetParticles(particles);
        }
    }
}
