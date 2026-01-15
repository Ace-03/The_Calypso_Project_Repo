using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 gravity;

    private void Awake()
    {
        if (!TryGetComponent<Rigidbody>(out rb))
        {
            Debug.LogWarning($"Custom Gravity cannot find Rigidbody on {this}, Adding Rigidbody");
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        gravity = Physics.gravity * rb.mass;
    }

    private void FixedUpdate()
    {
        rb.AddForce(gravity);
    }

    public void SetGravity(float mult)
    {
        gravity = Physics.gravity * rb.mass * mult;
    }
}
