using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class MineTossBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float launchForce;
    [SerializeField] private float range;
    [SerializeField] private float detonationTime;
    [SerializeField] private float volleyRate;
    [Tooltip("How far into the future the weapon will try to predict the target position")]
    [SerializeField] private float predictionTime;
    [SerializeField] private float gravityMultiplier;

    private Vector3 appliedGravity;
    private Vector3 currentAimVector = Vector3.one;

    private DamageSource damageSource = new DamageSource();
    private List<Transform> targetList = new List<Transform>();

    private IEnumerator ThrowBomb(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();

        for (int i = 0; i < volleyCount; ++i)
        {
            UpdateEnemyList();
            Transform target = GetClosestEnemy(targetList);
            if (!CheckRange(target.position))
            {
                continue;
            }

            GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);

            CustomGravity grav = newBomb.AddComponent<CustomGravity>();
            grav.SetGravity(gravityMultiplier);
            appliedGravity = grav.gravity;

            if (!newBomb.TryGetComponent<Rigidbody>(out Rigidbody bombRb))
            {
                Debug.LogError("No RigidBody On Bomb Prefab");
            }

            Nullable<Vector3> aimVector = GetAimVector(target);
            if (aimVector.HasValue)
            {
                currentAimVector = aimVector.Value;
            }

            bombRb.linearVelocity = Vector3.zero;
            bombRb.AddForce(currentAimVector.normalized * launchForce, ForceMode.Impulse);

            StartCoroutine(ExplodeBomb(newBomb, weapon));
            yield return new WaitForSeconds(volleyRate);
        }
    }

    private IEnumerator ExplodeBomb(GameObject bomb, WeaponController weapon)
    {
        yield return new WaitForSeconds(detonationTime);

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

        Destroy(explosion);
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        damageSource = weapon.GetDamageSource();
    }

    public void Attack(WeaponController weapon)
    {
        Debug.Log("Trying to throw mines");
        StartCoroutine(ThrowBomb(weapon));
    }

    public bool IsAimable()
    {
        return false;
    }

    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    void UpdateEnemyList()
    {
        List<AI_NAV> navAgents = new List<AI_NAV>();

        navAgents.AddRange(FindObjectsByType<AI_NAV>(FindObjectsSortMode.None));
        targetList.Clear();
        foreach (AI_NAV aI_NAV in navAgents)
        {
            targetList.Add(aI_NAV.transform);
        }
    }

    bool CheckRange(Vector3 targetPos)
    {
        float distance = Vector3.Distance(targetPos, transform.position);
        return !(distance > range);
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
}
