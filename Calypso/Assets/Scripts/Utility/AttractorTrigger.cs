using System.Collections;
using UnityEngine;

public class AttractorTrigger : MonoBehaviour
{
    [SerializeField] private float attractForce = 10f;
    [SerializeField] private string attractorTag = "Item";

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

            rb.AddForce(direction * attractForce, ForceMode.Acceleration);
        }
    }
}
