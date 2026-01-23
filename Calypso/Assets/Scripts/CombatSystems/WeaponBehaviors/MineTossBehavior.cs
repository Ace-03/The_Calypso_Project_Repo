using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MineTossBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float launchForce;
    [SerializeField] private float range;
    [SerializeField] private float detonationTime;
    [SerializeField] private float volleyDuration;
    [Tooltip("How far into the future the weapon will try to predict the target position")]
    [SerializeField] private float predictionTime;
    [SerializeField] private float gravityMultiplier;

    private Vector3 appliedGravity;
    private Vector3 currentAimVector = Vector3.one;

    private DamageSource damageSource = new DamageSource();

    private IEnumerator ThrowBomb(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();
        float volleyRate = volleyDuration / volleyCount;


        for (int i = 0; i < volleyCount; ++i)
        {
            Transform target = TargetCalculator.GetClosestEnemy(transform.position);
            Nullable<Vector3> aimVector = transform.position;

            if (target == null)
            {
                /* throw mine in random directions
                Vector3 targetPos = transform.position + new Vector3(
                UnityEngine.Random.Range(-5f, 5f),
                0,
                UnityEngine.Random.Range(-5f, 5f));
                

                aimVector = GetAimVector(targetPos);
                if (aimVector.HasValue)
                    currentAimVector = aimVector.Value;
                */
                continue;
            }
            else
            {
                if (!TargetCalculator.CheckRange(target.position, transform.position, range))
                    continue;

                aimVector = GetAimVector(target);
                if (aimVector.HasValue)
                    currentAimVector = aimVector.Value;
            }

            GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

            CustomGravity grav = newBomb.AddComponent<CustomGravity>();
            grav.SetGravity(gravityMultiplier);
            appliedGravity = grav.gravity;

            if (!newBomb.TryGetComponent<Rigidbody>(out Rigidbody bombRb))
            {
                Debug.LogError("No RigidBody On Bomb Prefab");
            }

            bombRb.linearVelocity = Vector3.zero;
            bombRb.AddForce(currentAimVector.normalized * launchForce, ForceMode.Impulse);
            Debug.Log($"Aim Vector is {aimVector}");

            StartCoroutine(ExplodeBomb(newBomb, weapon));
            yield return new WaitForSeconds(volleyRate);
        }
    }

    private IEnumerator ExplodeBomb(GameObject bomb, WeaponController weapon)
    {
        yield return new WaitForSeconds(detonationTime);

        ParticleSystem ps = bomb.GetComponentInChildren<ParticleSystem>();
        ps.transform.parent = null;
        Destroy(ps, 5f);

        Destroy(bomb);
        GameObject explosion = Instantiate(explosionPrefab, bomb.transform.position, Quaternion.identity);
        explosion.transform.localScale *= weapon.GetArea();

        if (!explosion.TryGetComponent<Collider>(out Collider explosionCol))
        {
            Debug.LogError("Could Not Find Collider on explosion");
        }
        else
        {
            GeneralModifier.UpdateCollisionLayers(explosionCol, weapon.team);
        }

        if (!explosion.TryGetComponent<BulletTrigger>(out BulletTrigger explosionTrigger))
        {
            Debug.LogError("Explosion is missing bullet trigger");
        }
        else
        {
            explosionTrigger.SetDamageSource(damageSource);
        }

        yield return new WaitForSeconds(weapon.GetDuration());

        explosion.GetComponent<Collider>().enabled = false;

        ParticleSystem expPs = explosion.GetComponentInChildren<ParticleSystem>();
        if (expPs != null) expPs.transform.parent = null;
        Destroy(expPs, 5f);

        Destroy(explosion);
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        damageSource = weapon.GetDamageSource();
    }

    public void Attack(WeaponController weapon)
    {
        StartCoroutine(ThrowBomb(weapon));
    }

    public bool IsAimable()
    {
        return false;
    }

    private Nullable<Vector3> GetAimVector(Transform target)
    {
        Vector3 pos = target.position;

        if (target.TryGetComponent<NavMeshAgent>(out NavMeshAgent navAgent))
        {
            pos = navAgent.transform.position + (navAgent.velocity * predictionTime);
            Debug.Log("trying future position");
        }

        FiringSolution fs = new FiringSolution();
        fs.useMaxTime = true;
        return fs.Calculate(transform.position, pos, launchForce, appliedGravity);
    }
    private Nullable<Vector3> GetAimVector(Vector3 target)
    {
        FiringSolution fs = new FiringSolution();
        fs.useMaxTime = true;
        return fs.Calculate(transform.position, target, launchForce, appliedGravity);
    }
}
