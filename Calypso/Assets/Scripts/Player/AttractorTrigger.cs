using System.Collections;
using UnityEngine;

public class AttractorTrigger : MonoBehaviour
{
    [SerializeField] private float attractForce = 10f;
    [SerializeField] private float windUpForce = 5f;
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

        while (other.gameObject != null)
        {
            direction = (transform.position - other.position).normalized;
            rb.AddForce(direction * attractForce, ForceMode.VelocityChange);
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetAttractorRadius(StatSystem stats)
    {
        col.radius = stats.GetFinalValue(StatType.ItemAttraction);
        Debug.Log($"Attractor Radius updated to {col.radius}");
    }
}
