using TMPro;
using UnityEngine;

public class UpgradeResultsScreen : MonoBehaviour
{
    [SerializeField] private OnUpgradeAttemptEventSO upgradeEvent;

    [Header("Components")]
    [SerializeField] private GameObject screenParent;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI detailsText;

    private void OnEnable()
    {
        upgradeEvent.RegisterListener(DisplayResultScreen);
    }

    private void OnDisable()
    {
        upgradeEvent.UnregisterListener(DisplayResultScreen);

    }

    private void DisplayResultScreen(UpgradeAttemptPayload payload)
    {
        resultText.text = payload.Result;
        detailsText.text = payload.details;
        screenParent.SetActive(true);
    }

    public void HideResultsScreen()
    {
        screenParent.SetActive(false);
    }
}
