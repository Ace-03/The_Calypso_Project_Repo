using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform playerAimer;

    private float baseMaxSpeed;
    private float baseAcceleration;
    private float baseDeceleration;
    private float slideClamp;

    private float maxSpeed;
    private float acceleration;
    private float deceleration;

    private float rbSpeedAdjustment = 20f;
    private Rigidbody rb;
    private IInteractable currentInteractable;
    private Vector3 movementVector;
    private Vector3 aimVector;

    private void Start()
    {
        if (!TryGetComponent<Rigidbody>(out rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        InitializeMovementStats();
        ApplyMovementModifiers();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateAim();
    }

    private void UpdateAim()
    {
        if (aimVector.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(aimVector, Vector3.up);
            playerAimer.rotation = Quaternion.Slerp(playerAimer.rotation, targetRotation, 0.2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentInteractable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit(Collider other)
    {
        currentInteractable = null;
    }

    public void SetMovementVector(Vector3 direction)
    {
        movementVector = direction;
    }

    public void SetAimerRotation(Vector3 direction)
    {
        aimVector = direction;
    }

    public void Interact()
    {
        currentInteractable?.Interact();
    }

    private void MovePlayer()
    {
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (movementVector.magnitude > 0)
        {
            Vector3 targetVelocity = movementVector * maxSpeed;
            Vector3 velocityChange = targetVelocity - horizontalVelocity;
            rb.AddForce(velocityChange * acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else if (movementVector.magnitude <= 0.15f && horizontalVelocity.magnitude > slideClamp)
        {
            Vector3 brakingForce = -horizontalVelocity.normalized * deceleration * (rbSpeedAdjustment / 3);
            rb.AddForce(brakingForce, ForceMode.Acceleration);
        }

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = horizontalVelocity.normalized * maxSpeed;
        }

        if (horizontalVelocity.magnitude <= slideClamp)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    private void InitializeMovementStats()
    {
        baseMaxSpeed = PlayerStats.Instance.GetMaxSpeed();
        baseAcceleration = PlayerStats.Instance.GetAcceleration();
        baseDeceleration = PlayerStats.Instance.GetDeceleration();
        slideClamp = 0.3f;
    }

    public void ApplyMovementModifiers()
    {
        maxSpeed = baseMaxSpeed * PlayerStats.Instance.GetSpdModifier();
        acceleration = baseAcceleration * PlayerStats.Instance.GetAccModifier();
        deceleration = baseDeceleration * PlayerStats.Instance.GetDecelModifier();
    }
}
