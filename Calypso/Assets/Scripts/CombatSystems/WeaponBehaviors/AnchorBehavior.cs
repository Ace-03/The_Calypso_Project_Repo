using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private GameObject anchorPrefab;
    [SerializeField] private float volleyRate;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float dropDelay;

    private DamageSource damageSource;

    private IEnumerator ThrowAnchor(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();

        for (int i = 0; i < volleyCount; ++i)
        {
            GameObject newAnchor = Instantiate(anchorPrefab, transform.position, Quaternion.identity);
            CustomGravity grav = newAnchor.AddComponent<CustomGravity>();
            if (!newAnchor.TryGetComponent<Rigidbody>(out Rigidbody anchorRb))
            {
                Debug.LogError("No RigidBody On Anchor Prefab");
            }
            if (!newAnchor.TryGetComponent<BulletTrigger>(out BulletTrigger anchorTrigger))
            {
                Debug.LogError($"No Bullet Trigger Found On Anchor Prefab");
            }

            grav.SetGravity(gravityMultiplier);
            anchorTrigger.SetDamageSource(damageSource);
            anchorRb.AddForce(Vector3.up * 100, ForceMode.Impulse);
            newAnchor.transform.eulerAngles = new Vector3(0, 0, 180);

            StartCoroutine(DropAnchor(newAnchor, weapon));
            yield return new WaitForSeconds(volleyRate);
        }
    }

    private IEnumerator DropAnchor(GameObject anchorObject, WeaponController weapon)
    {
        yield return new WaitForSeconds(dropDelay);

        Transform target = TargetCalculator.GetClosestEnemy(transform.position);

        anchorObject.transform.position = target.position + Vector3.up * 50f;
        anchorObject.transform.rotation = Quaternion.identity;
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        damageSource = weapon.GetDamageSource();
    }

    public void Attack(WeaponController weapon)
    {
        StartCoroutine(ThrowAnchor(weapon));
    }

    public bool IsAimable()
    {
        return false;
    }
}
