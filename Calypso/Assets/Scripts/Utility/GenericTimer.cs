using UnityEngine;

public class GenericTimer : MonoBehaviour
{
    public OnGenericEventSO timerEvent;
    public float resetTime;

    private float currentTime;

    private void Awake()
    {
        currentTime = 0;
    }

    public void Initialize(OnGenericEventSO eventObject, float TimeToReset)
    {
        timerEvent = eventObject;
        resetTime = TimeToReset;
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime > resetTime)
        {
            currentTime = 0;
            timerEvent.Raise(new GameEventPayload());
        }
    }
}
