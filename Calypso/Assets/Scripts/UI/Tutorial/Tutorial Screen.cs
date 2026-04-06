using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private CanvasGroup baseCanvasGroup;

    public List<string> tutorialMessages;

    private int index = 0;

    public void toggleTutorial(bool toggle)
    {
        canvasObject.SetActive(toggle);
        index = 0;
        displayText(index);

        PlayerManager.Instance.ToggleMenuControls(!toggle);
        baseCanvasGroup.interactable = !toggle;
        baseCanvasGroup.blocksRaycasts = !toggle;

        if (toggle == true)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void AdvanceText()
    {
        index++;
        displayText(index);
    }

    public void EndTutorial()
    {
        index = tutorialMessages.Count;
        displayText(index);
    }

    private void displayText(int index)
    {
        if (index >= tutorialMessages.Count)
        {
            Debug.Log("Tutorial Messages Exhausted");
            toggleTutorial(false);
            return;
        }

        textField.text = tutorialMessages[index];
    }

    public void SetMessages(List<string> messages)
    {
        tutorialMessages.Clear();
        tutorialMessages = messages;
    }
}
