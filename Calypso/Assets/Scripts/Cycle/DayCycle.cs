using System;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float dayTime = 120f;
    public float nightTime = 120f;
    private float cycleClock = 0f;

    private float currentTimeCheck = 0f;
    private LightingState lightingSate = LightingState.sunrise;
    bool isDayTime = true;
    
    void Start()
    {
        StartDay();
    }

    void Update()
    {
        UpdateClock();
        UpdateLighting();
    }
    void UpdateClock()
    {
        cycleClock += Time.deltaTime;

        UpdateLighting();

        if (CheckTimer(cycleClock, currentTimeCheck))
        {
            ChangeDayState();
        }
    }

    private void UpdateLighting()
    {

    }

    private void StartDay()
    {
        // Code to Activate Day
        isDayTime = true;
        currentTimeCheck = dayTime;
        Debug.Log("Day time. Please Implement Daytime Code.");
    }
    private void StartNight()
    {
        // Code to Activate Night
        isDayTime = false;
        currentTimeCheck = nightTime;
        Debug.Log("Night time. Please Implement Nighttime Code.");
    }

    private void ChangeDayState()
    {
        if (isDayTime)
        {
            StartNight();
        }
        else
        {
            StartDay();
        }

        ResetTimer();
    }

    private bool CheckTimer(float timer, float ValueToCheck)
    {
        return timer > ValueToCheck;
    }

    private void ResetTimer()
    {
        cycleClock = 0f;
    }
}
