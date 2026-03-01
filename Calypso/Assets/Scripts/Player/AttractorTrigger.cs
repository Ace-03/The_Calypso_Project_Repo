using System.Collections;
using UnityEngine;

public class AttractorTrigger : MonoBehaviour
{
    [SerializeField] private float attractForce = 10f;
    [SerializeField] private float windUpForce = 5f;
    [SerializeField] private float dampening = 3f;
    [SerializeField] private string attractorTag = "Item";

    private SphereCollider col;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        SetAttractorRadius(ContextRegister.Instance.GetContext().statSystem);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(attractorTag))
        {
            Pickup pickup = other.GetComponent<Pickup>();

            if (pickup != null)
            {
                if (pickup.delayAttraction == true) return;
            }

            if (!other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Debug.Log($"Item: {other.name} is missing a rigidbody and will fail to attract to player");
                return;
            }

            if (!pickup.attractionStarted)
                StartCoroutine(pullItem(rb, other.transform));
        }
    }

    private IEnumerator pullItem(Rigidbody rb, Transform other)
    {
        Vector3 direction = (transform.position - other.position).normalized;
        rb.AddForce(-direction * windUpForce, ForceMode.Impulse);

        Vector3 velocityToTarget = Vector3.Project(rb.linearVelocity, direction);
        Vector3 lateralVelocity = rb.linearVelocity - velocityToTarget;

        while (other != null)
        {
            direction = (transform.position - other.position).normalized;
            rb.AddForce(direction * attractForce, ForceMode.VelocityChange);
            rb.AddForce(-lateralVelocity * dampening, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetAttractorRadius(StatSystem stats)
    {
        col.radius = stats.GetFinalValue(StatType.ItemAttraction);
    }
}
