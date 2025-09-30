using System.Collections;
using UnityEngine;

public class AttractorTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private float attractForce = 10f;
    [SerializeField] 
    private string attractorTag = "Player";

    private Rigidbody rb;

    private void Awake()
    {
        if (parentTransform == null && rb == null)
        {
            GameObject parent = transform.parent.gameObject;

            parentTransform = parent.transform;

            if (!parent.TryGetComponent<Rigidbody>(out rb))
                Debug.LogWarning(parent.name + " has No Rigidbody and needs one for pickup functoinality. please check prefab");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(attractorTag))
        {
            StartCoroutine(AttractTo(other.transform));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(attractorTag))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator AttractTo(Transform transform)
    {
        while (true)
        {
            Debug.Log("In Attractor Range");
            Vector3 direction = (transform.position - parentTransform.position).normalized;
            float attractForce = this.attractForce;
            
            if (parentTransform.TryGetComponent<Rigidbody>(out Rigidbody rb))
                rb.AddForce(direction * attractForce, ForceMode.Acceleration);

            yield return new WaitForFixedUpdate();
        }

    }
}
