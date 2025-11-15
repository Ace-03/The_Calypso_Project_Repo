using System.Collections;
using UnityEngine;

public class AttractorTrigger : MonoBehaviour
{
    [SerializeField] private float attractForce = 10f;
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
            if (!other.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Debug.Log($"Item: {other.name} is missing a rigidbody and will fail to attract to player");
                return;
            }

            Vector3 direction = (transform.position - other.transform.position).normalized;

            rb.AddForce(direction * attractForce, ForceMode.VelocityChange);
        }
    }

    public void SetAttractorRadius(StatSystem stats)
    {
        col.radius = stats.GetFinalValue(StatType.ItemAttraction);
        Debug.Log($"Attractor Radius updated to {col.radius}");
    }
}
