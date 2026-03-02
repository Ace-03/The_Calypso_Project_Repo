using UnityEngine;
using UnityEngine.UI;

public class BlueprintScreen : MonoBehaviour
{
    [SerializeField] private OnBlueprintCollectedEventSO collectedEvent;
    [SerializeField] private Image itemIcon;
    [SerializeField] private GameObject screen;

    private void OnEnable()
    {
        collectedEvent.RegisterListener(ShowScreen);
    }

    private void OnDisable()
    {
        collectedEvent.UnregisterListener(ShowScreen);
    }

    private void ShowScreen(BlueprintCollectedPayload payload)
    {
        Time.timeScale = 0f;
        itemIcon.sprite = payload.icon;
        SpriteNormalizer.NormalizeImage(itemIcon.gameObject);
        screen.SetActive(true);
        HudManager.Instance.ToggleHud(false);
    }

    public void HideScreen()
    {
        Time.timeScale = 1f;
        screen.SetActive(false);
        HudManager.Instance.ToggleHud(true);
    }
}
