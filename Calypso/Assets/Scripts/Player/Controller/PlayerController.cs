using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float bobAmmount = 0.5f;
    [SerializeField] private float bobSpeed = 0.1f;
    [SerializeField] private spriteControllerData spriteData;
    [SerializeField] private OnGenericEventSO clearInteractableEvent;

    private Transform weaponPivot;

    private float maxSpeed;
    private float acceleration;
    private float deceleration;
    private float bobTimer = 0f;

    private PlayerSpriteController spriteController = new PlayerSpriteController();
    private Rigidbody rb;
    private IInteractable currentInteractable;
    private Vector3 movementVector;
    private Vector3 aimVector;
    private Vector3 horizontalVelocity;

    private void Start()
    {
        if (!TryGetComponent<Rigidbody>(out rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        RecalculateMovementStats(ContextRegister.Instance.GetContext().statSystem);
        weaponPivot = ContextRegister.Instance.GetContext().playerManager.GetPrimaryWeapon().weaponPivot;
        spriteController.Initialize(spriteData);
        spriteController.bobAmmount = bobAmmount / 100;

        Invoke("ClearInteractable", 0.05f);
    }

    private void OnDisable()
    {
        if (rb != null) rb.linearVelocity = Vector3.zero;
        clearInteractableEvent.UnregisterListener(ClearInteractable);
    }

    private void OnEnable()
    {
        if (rb != null) rb.linearVelocity = Vector3.zero;
        clearInteractableEvent.RegisterListener(ClearInteractable);
    }

    private void FixedUpdate()
    {
        horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        MovePlayer();
        UpdateSprite();
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

    private void ClearInteractable(GameEventPayload payload)
    {
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

    public void PauseGame()
    {
        bool isPaused = PauseManager.instance.isPaused;
        PauseManager.instance.PauseGame(!isPaused);
    }

    private void MovePlayer()
    {
        if (movementVector.magnitude > 0.15f)
        {
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

    private void UpdateSprite()
    {
        if (movementVector.magnitude >= 0.15f)
        {
            spriteController.SetSprite(movementVector);
            TryBobbing();
        }
        else
        {
            spriteController.StopBob();
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

    public void ToggleCollider(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }

    public void RecalculateMovementStats(StatSystem stats)
    {
        maxSpeed = stats.GetFinalValue(StatType.MaxSpeed);
        acceleration = stats.GetFinalValue(StatType.Accel);
        deceleration = stats.GetFinalValue(StatType.Decel);
    }

    private void TryBobbing()
    {
        bobTimer += Time.deltaTime;
        if (bobTimer >= bobSpeed)
        {
            bobTimer = 0;
            spriteController.BobSprite();
        }
    }

    private void ClearInteractable()
    {
        currentInteractable = null;
    }

    public void UpdateWeaponPivot()
    {
        weaponPivot = ContextRegister.Instance.GetContext().playerManager.GetPrimaryWeapon().weaponPivot;
    }

    public bool GetFacingRight()
    {
        return spriteController.GetFacingRight();
    }

    public bool GetFacingUp()
    {
        return spriteController.GetFacingUp();
    }
}