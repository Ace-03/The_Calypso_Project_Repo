using UnityEngine;

public class BossExitTrigger : MonoBehaviour
{
    [SerializeField] private BossController controller;

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Boss is No Longer Active");

        if (other.CompareTag("Player"))
            controller.ActivateBoss(false);
    }

}
