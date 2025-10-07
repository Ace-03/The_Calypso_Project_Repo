using UnityEngine;

public class BillboardController : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cameraTransform != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraTransform.forward);
            transform.rotation = targetRotation;
        }
    }
}