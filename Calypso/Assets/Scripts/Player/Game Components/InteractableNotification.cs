using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(PlayerController))]
public class InteractableNotification : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject notifierCanvas;
    [SerializeField] private Image notifIcon;

    [Header("Resources")]
    [SerializeField] private Sprite miningSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite handSprite;

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

        if (currentInteractable == null)
        {
            notifierCanvas.SetActive(false);

        }
        else if (currentInteractable.GetGameObject().GetComponent<BaseInteractable>() != null)
        {
            SetNotification(openSprite);
        }
        else if (currentInteractable.GetGameObject().GetComponent<MiningInteractable>() != null)
        {
            SetNotification(miningSprite);
        }
        else
        {
            notifierCanvas.SetActive(false);
        }
    }

    private void SetNotification(Sprite sp)
    {
        notifierCanvas.SetActive(true);
        notifIcon.sprite = sp;
        SpriteNormalizer.NormalizeImage(notifIcon.gameObject);
    }
}
