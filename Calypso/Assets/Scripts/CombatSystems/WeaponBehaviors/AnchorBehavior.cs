using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnchorBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private GameObject anchorPrefab;
    [SerializeField] private float volleyDuration;
    [SerializeField] private float gravityMultiplier;
    [SerializeField] private float throwStrength;
    [SerializeField] private float dropDelay;
    [Tooltip("How far into the future the weapon will try to predict the target position")]
    [SerializeField] private float predictionTime;

    private DamageSource damageSource;

    private IEnumerator ThrowAnchor(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();
        float volleyRate = volleyDuration / volleyCount;

        for (int i = 0; i < volleyCount; ++i)
        {
            if (TargetCalculator.GetClosestEnemy(transform.position) == null) continue;

            GameObject newAnchor = Instantiate(anchorPrefab, transform.position + Vector3.up, Quaternion.identity);

            // failsafe destroy
            Destroy(newAnchor, 15f);

            CustomGravity grav = newAnchor.AddComponent<CustomGravity>();
            if (!newAnchor.TryGetComponent<Rigidbody>(out Rigidbody anchorRb))
            {
                Debug.LogError("No RigidBody On Anchor Prefab");
            }
            if (!newAnchor.TryGetComponent<BulletTrigger>(out BulletTrigger anchorTrigger))
            {
                Debug.LogError($"No Bullet Trigger Found On Anchor Prefab");
            }
            if (!newAnchor.TryGetComponent<Collider>(out Collider anchorCol))
            {
                Debug.LogError($"No collider Found On Anchor Prefab");
            }

            Debug.Log("Tossing Anchor");

            GeneralModifier.UpdateCollisionLayers(anchorCol, weapon.team);
            grav.SetGravity(0);
            anchorTrigger.SetDamageSource(damageSource);
            anchorRb.AddForce(Vector3.up * throwStrength, ForceMode.Impulse);
            newAnchor.transform.eulerAngles = new Vector3(0, 0, 180);

            ComponentController componentController = newAnchor.GetComponent<ComponentController>();
            componentController.GetShadow().SetActive(false);

            StartCoroutine(DropAnchor(newAnchor, weapon));
            yield return new WaitForSeconds(volleyRate);
        }
    }

    private IEnumerator DropAnchor(GameObject anchorObject, WeaponController weapon)
    {
        yield return new WaitForSeconds(dropDelay);

        anchorObject.GetComponent<CustomGravity>().SetGravity(gravityMultiplier);
        Transform target = TargetCalculator.GetRandomOfClosestEnemies(transform.position);
        Vector3 targetPos = Vector3.zero;

        if (target == null)
        {
            /* throw anchor in random directions
            Debug.LogWarning("Cannot Find Target, Generating Random position");
            targetPos = transform.position + new Vector3(
                UnityEngine.Random.Range(-5f, 5f), 
                0, 
                UnityEngine.Random.Range(-5f, 5f));
            */

            yield break;
        }
        else
        {
            if (target.TryGetComponent<NavMeshAgent>(out NavMeshAgent navAgent))
            {
                targetPos = navAgent.transform.position + (navAgent.velocity * predictionTime);
                Debug.Log("trying future position");
            }
            else
            {
                targetPos = target.position;
            }
        }
        ComponentController componentController = anchorObject.GetComponent<ComponentController>();
        componentController.GetShadow().SetActive(true);

        anchorObject.transform.localScale *= weapon.GetArea();
        anchorObject.transform.position = targetPos + Vector3.up * 40f;
        anchorObject.GetComponent<Rigidbody>().linearVelocity = Vector3.down * 10;
        anchorObject.transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(weapon.GetDuration());
        componentController.GetModel().SetActive(false);
        componentController.GetShadow().SetActive(false);
        componentController.GetTrigger().enabled = false;
        Destroy(anchorObject, 5f);
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        damageSource = weapon.GetDamageSource();
    }

    public void Attack(WeaponController weapon)
    {
        StartCoroutine(ThrowAnchor(weapon));
    }

    public bool IsAimable() => false;

    protected virtual Transform GetTarget() => TargetCalculator.GetRandomOfClosestEnemies(transform.position);
}
