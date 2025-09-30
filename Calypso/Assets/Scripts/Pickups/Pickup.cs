using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    private SphereCollider pickupTrigger;
    [SerializeField]
    private SphereCollider attractorTrigger;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer sr;


    private PickupSO pickupData;
    private Rigidbody rb;


    void Awake()
    {
        SetUpComponents();
        LaunchPickup();
    }


    void SetUpComponents()
    {
        if (pickupTrigger == null)
        {
            GameObject triggerObject = Instantiate(new GameObject(), transform);
            triggerObject.name = "PickupTrigger";
            triggerObject.AddComponent<PickupTrigger>();
            pickupTrigger = triggerObject.AddComponent<SphereCollider>();
            pickupTrigger.isTrigger = true;
            pickupTrigger.radius = 0.6f;
        }

        if (attractorTrigger == null)
        {
            GameObject triggerObject = Instantiate(new GameObject(), transform);
            triggerObject.name = "AttractorTrigger";
            triggerObject.AddComponent<AttractorTrigger>();
            attractorTrigger = triggerObject.AddComponent<SphereCollider>();
            attractorTrigger.isTrigger = true;
            attractorTrigger.radius = 12f;
        }

        if (sr == null)
        {
            GameObject sprite = Instantiate(new GameObject(), transform);
            sprite.name = "Sprite";
            sr = sprite.AddComponent<SpriteRenderer>();
            sr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            sr.receiveShadows = true;

            if (animator == null)
            {
                animator = sprite.AddComponent<Animator>();
                // animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/PickupAnimatorController");
            }
        }

        if (!TryGetComponent<Rigidbody>(out rb))
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
            rb.linearDamping = 0.5f;
        }

        PlayVisualAnimation();
    }

    public void InitializeData()
    {
        sr.sprite = pickupData.sprite;
    }

    private void PlayVisualAnimation()
    {
        animator.Play("PickupIdle");
    }

    public void LaunchPickup()
    {
        Vector3 LaunchDirection = new Vector3(Random.Range(-0.5f, 0.5f), 1f, Random.Range(-0.5f, 0.5f)).normalized;
        float LaunchForce = Random.Range(2f, 5f) * 2;
        rb.AddForce(LaunchDirection * LaunchForce, ForceMode.Impulse);
    }

    public PickupSO GetPickupData()
    {
        return pickupData;
    }

    public void SetPickupData(PickupSO data)
    {
        pickupData = data;
    }

    public abstract void CollectPickup(PickupSO data);

}


public enum ResourceType
{
    Resin,
    Stone,
}