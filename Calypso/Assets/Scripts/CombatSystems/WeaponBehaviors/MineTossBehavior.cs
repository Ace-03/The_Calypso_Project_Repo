using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTossBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float launchForce;
    [SerializeField] private float range;
    [SerializeField] private float detonationTime;
    [SerializeField] private float volleyRate;

    private DamageSource damageSource;
    private List<Transform> targetList = new List<Transform>();

    private IEnumerator ThrowBomb(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();
        float sizeModifier = weapon.GetArea();
        float duration = weapon.GetDuration();

        for (int i = 0; i < volleyCount; ++i)
        {
            yield return new WaitForSeconds(volleyRate);

            UpdateEnemyList();
            Transform target = GetClosestEnemy(targetList);
            if (!CheckRange(target.position))
            {
                continue;
            }

            Nullable<Vector3> aimVector = GetAimVector(target);

            GameObject newBomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            if (!newBomb.TryGetComponent<Rigidbody>(out Rigidbody bombRb))
            {
                Debug.LogError("No RigidBody On Bomb Prefab");
            }

            if (aimVector.HasValue)
            {
                bombRb.linearVelocity = Vector3.zero;
                bombRb.AddForce(aimVector.Value.normalized * launchForce, ForceMode.Impulse);

                Collider col = newBomb.GetComponentInChildren<Collider>();
                col.enabled = false;
                yield return new WaitForSeconds(detonationTime);
                col.enabled = true;
            }

            StartCoroutine(ExplodeBomb(newBomb, sizeModifier, duration));
        }
    }

    private IEnumerator ExplodeBomb(GameObject bomb, float sizeMod, float duration)
    {
        yield return new WaitForSeconds(detonationTime);

        Destroy(bomb);
        GameObject explosion = Instantiate(explosionPrefab, bomb.transform.position, Quaternion.identity);
        explosion.transform.localScale *= sizeMod;

        if (!explosion.TryGetComponent<BulletTrigger>(out BulletTrigger explosionTrigger))
        {
            Debug.LogError("Explosion is missing bullet trigger");
        }

        explosionTrigger.SetDamageSource(damageSource);

        yield return new WaitForSeconds(duration);

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
        FiringSolution fs = new FiringSolution();
        fs.useMaxTime = true;
        return fs.Calculate(transform.position, target.transform.position, launchForce, Physics.gravity);
    }
}
