using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{
    [SerializeField] private BossController controller;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boss is now Active");

        if (other.CompareTag("Player"))
            controller.ActivateBoss(true);
    }

}
