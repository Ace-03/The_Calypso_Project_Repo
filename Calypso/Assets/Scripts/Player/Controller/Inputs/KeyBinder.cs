using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBinder : MonoBehaviour
{
    public Text jumpButtonText; // UI Text showing the current key
    private KeyCode jumpKey;
    private bool isWaitingForKey = false;

    void Start()
    {
        // Load saved key or default to Space
        jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpKey", "Space"));
        jumpButtonText.text = jumpKey.ToString();
    }

    void OnGUI()
    {
        if (isWaitingForKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                UpdateKey(e.keyCode);
                isWaitingForKey = false;
            }
        }
    }

    public void StartAssignment()
    {
        isWaitingForKey = true;
        jumpButtonText.text = "Press any key...";
    }

    void UpdateKey(KeyCode newKey)
    {
        jumpKey = newKey;
        jumpButtonText.text = jumpKey.ToString();
        PlayerPrefs.SetString("JumpKey", jumpKey.ToString());
        Debug.Log("Bound to: " + newKey);
    }
}