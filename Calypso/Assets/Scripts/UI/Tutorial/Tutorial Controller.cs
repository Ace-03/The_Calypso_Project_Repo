using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private OnTutorialTriggerEventSO tutorialTrigger;

    [Header("Components")]
    [SerializeField] private TutorialScreen tutScreen;

    [Header("Messages")]
    [SerializeField] private List<TutorialSequence> tutorials;


    private void OnEnable()
    {
        tutorialTrigger.RegisterListener(RunTutorialOnEvent);
    }

    private void OnDisable()
    {
        tutorialTrigger.UnregisterListener(RunTutorialOnEvent);
    }

    private void Start()
    {
        RunTutorial(0);
    }

    private void RunTutorial(int index)
    {
        tutScreen.SetMessages(tutorials[index].messages);
        tutScreen.toggleTutorial(true);
        tutorials[index].cleared = true;
    }

    private void RunTutorialOnEvent(TutorialTriggerPayload payload)
    {
        if (tutorials[payload.tutorialNumber].cleared)
        {
            Debug.LogWarning("This tutorial has already been cleared.");
            return;
        }

        RunTutorial(payload.tutorialNumber);
    }
}
