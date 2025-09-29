using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField]
    private SphereCollider pickupTrigger;
    [SerializeField]
    private SphereCollider attractorTrigger;

    private Rigidbody rb;
    private PickupSO pickupData;
    private Animator animator;
    
    void Awake()
    {
        Initialize();
        LaunchPickup();
    }


    void Initialize()
    {
        if (pickupTrigger == null)
        {
            GameObject triggerObject = Instantiate(new GameObject(), transform);
            triggerObject.name = "PickupTrigger";
            pickupTrigger = triggerObject.AddComponent<SphereCollider>();
            pickupTrigger.isTrigger = true;
            pickupTrigger.radius = 0.6f;
        }

        if (pickupTrigger == null)
        {
            GameObject triggerObject = Instantiate(new GameObject(), transform);
            triggerObject.name = "AttractorTrigger";
            attractorTrigger = triggerObject.AddComponent<SphereCollider>();
            attractorTrigger.isTrigger = true;
            attractorTrigger.radius = 3.5f;
        }

        rb = gameObject.AddComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.linearDamping = 0.5f;

        PlayVisualAnimation();
    }

    private void PlayVisualAnimation()
    {
        animator.Play("PickupIdle");
    }

    public void LaunchPickup()
    {
        Vector3 LaunchDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
        float LaunchForce = Random.Range(2f, 5f) * 20;
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