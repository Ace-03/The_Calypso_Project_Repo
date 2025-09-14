using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    public float deceleration;

    private float speedMultiplier = 20f;
    private Rigidbody rb;
    private IInteractable currentInteractable;
    private Vector3 movementVector;

    private void Start()
    {
        if (!TryGetComponent<Rigidbody>(out rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (movementVector.magnitude > 0)
        {
            Vector3 targetVelocity = movementVector * maxSpeed;
            Vector3 velocityChange = targetVelocity - horizontalVelocity;  
            rb.AddForce(velocityChange * acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else if (movementVector.magnitude <= 0.15f && horizontalVelocity.magnitude > 0)
        {
            Vector3 brakingForce = -horizontalVelocity.normalized * deceleration * (speedMultiplier / 3);
            rb.AddForce(brakingForce, ForceMode.Acceleration);
        }

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = horizontalVelocity.normalized * maxSpeed;
        }
    }
    public void SetMovementVector(Vector3 newVector)
    {
        movementVector = newVector;
    }

    public void Interact()
    {
        currentInteractable?.Interact();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentInteractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentInteractable = null;
    }
}
