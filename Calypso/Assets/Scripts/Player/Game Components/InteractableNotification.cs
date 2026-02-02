using TMPro;
using UnityEngine;

[RequireComponent (typeof(PlayerController))]
public class InteractableNotification : MonoBehaviour
{
    [SerializeField] private GameObject notifierCanvas;
    [SerializeField] private TextMeshProUGUI notificationText;

    private PlayerController playerController;

    private void Awake()
    {
        if (playerController == null &&
            !TryGetComponent<PlayerController>(out playerController))
            Debug.LogError("No Player controller on Interactable Notification Component");
    }

    private void Update()
    {
        IInteractable currentInteractable = playerController.CurrentInteractable;

        if ( currentInteractable == null)
        {
            notifierCanvas.SetActive(false);
        }
        else if (currentInteractable.GetGameObject().GetComponent<BaseInteractable>() != null)
        {
            SetNotification("OPEN");
        }
        else if (currentInteractable.GetGameObject().GetComponent<MiningInteractable>() != null)
        {
            SetNotification("MINE");
        }
    }

    private void SetNotification(string message)
    {
        notifierCanvas.SetActive(true);
        notificationText.text = message;
    }
}
