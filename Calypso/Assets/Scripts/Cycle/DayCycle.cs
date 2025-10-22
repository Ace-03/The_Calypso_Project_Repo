using System;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float dayTime = 120f;
    public float nightTime = 120f;
    private float cycleClock = 0f;

    private float currentTimeCheck = 0f;
    private float currentLightingCheck = 0f;
    private LightingSate lightState = LightingSate.sunrise;
    bool isDayTime = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartDay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClock();
        UpdateLighting();
    }
    void UpdateClock()
    {
        cycleClock += Time.deltaTime;

        if (CheckTimer(cycleClock, currentLightingCheck))
        {
            UpdateLighting();
            UpdateLightCheck();
        }

        if (CheckTimer(cycleClock, currentTimeCheck))
        {
            ChangeDayState();
        }
    }

    private void UpdateLighting()
    {

    }

    private void UpdateLightCheck()
    {
        switch (lightState)
        {
            case LightingSate.sunrise:
                currentLightingCheck = currentTimeCheck * 0.25f;
                lightState = LightingSate.morning;
                break;
            case LightingSate.morning:
                currentLightingCheck = currentTimeCheck * 0.5f;
                lightState = LightingSate.daylight;
                break;
            case LightingSate.daylight:
                currentLightingCheck = currentTimeCheck * 0.75f;
                lightState = LightingSate.evening;
                break;
            case LightingSate.evening:
                currentLightingCheck = currentTimeCheck;
                lightState = LightingSate.sunset;
                break;
            case LightingSate.sunset:
                currentLightingCheck = currentTimeCheck * 0.25f;
                lightState = LightingSate.night;
                break;
            case LightingSate.night:
                currentLightingCheck = currentTimeCheck * 0f;
                lightState = LightingSate.sunrise;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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
