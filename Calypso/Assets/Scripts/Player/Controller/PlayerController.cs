using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform weaponPivot;

    [SerializeField]
    private playerSprites playerSprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float baseMaxSpeed;
    private float baseAcceleration;
    private float baseDeceleration;

    private float maxSpeed;
    private float acceleration;
    private float deceleration;

    private PlayerSpriteController spriteController = new PlayerSpriteController();
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
        spriteController.Initialize(spriteRenderer, playerSprites);
    }

    private void OnDisable()
    {
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }

    private void OnEnable()
    {
        if (rb != null) rb.linearVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateAim();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<IInteractable>(out var target);

        if (target != null)
            currentInteractable = target;
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractable == null) return;

        if (other.GetComponent<IInteractable>() == currentInteractable)
            currentInteractable = null;
    }

    public void SetMovementVector(Vector3 direction)
    {
        movementVector = direction;
    }

    public void SetAimVector(Vector3 direction)
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

        if (movementVector.magnitude > 0.15f)
        {
            spriteController.SetSprite(movementVector);

            Vector3 targetVelocity = movementVector * maxSpeed;
            Vector3 velocityChange = targetVelocity - horizontalVelocity;
            rb.AddForce(velocityChange * acceleration * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else if (movementVector.magnitude <= 0.15f)
        {
            Vector3 brakingForce = -horizontalVelocity.normalized * deceleration * Time.fixedDeltaTime;
            rb.AddForce(brakingForce, ForceMode.VelocityChange);
        
            if (horizontalVelocity.magnitude < 0.5f)
            {
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
        }

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = horizontalVelocity.normalized * maxSpeed;
        }
    }

    private void UpdateAim()
    {
        if (aimVector.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;
            weaponPivot.rotation = Quaternion.AngleAxis(angle, Vector3.down);
        }
    }

    private void InitializeMovementStats()
    {
        baseMaxSpeed = PlayerManager.Instance.GetMaxSpeed();
        baseAcceleration = PlayerManager.Instance.GetAcceleration();
        baseDeceleration = PlayerManager.Instance.GetDeceleration();
    }

    public void ApplyMovementModifiers()
    {
        maxSpeed = baseMaxSpeed * PlayerManager.Instance.GetSpdModifier();
        acceleration = baseAcceleration * PlayerManager.Instance.GetAccModifier();
        deceleration = baseDeceleration * PlayerManager.Instance.GetDecelModifier();
    }

    public void ToggleCollider(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }
}
