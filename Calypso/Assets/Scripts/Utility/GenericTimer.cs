using UnityEngine;

public class GenericTimer : MonoBehaviour
{
    public GenericTimerEventSO timerEvent;
    public float resetTime;

    private float currentTime;

    private void Awake()
    {
        currentTime = 0;
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;

        if (currentTime > resetTime)
        {
            currentTime = 0;
            timerEvent.Raise(new TimerPayload());
        }
    }
}
