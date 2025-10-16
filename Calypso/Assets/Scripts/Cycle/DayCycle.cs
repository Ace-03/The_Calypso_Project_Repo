using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float cycleClock = 0f;
    public float dayTime = 120f;
    public float nightTime = 120f;
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
    }
    void UpdateClock()
    {
        cycleClock += Time.deltaTime;
        if (cycleClock > dayTime && isDayTime)
        {
            StartNight();
        }
        if(cycleClock > dayTime+nightTime && !isDayTime)
        {
            cycleClock = 0f;
            StartDay();
        }
    }

    void StartDay()
    {
        // Code to Activate Day
        isDayTime = true;
        Debug.Log("Day time. Please Implement Daytime Code.");
    }
    void StartNight()
    {
        // Code to Activate Night
        isDayTime = false;
        Debug.Log("Night time. Please Implement Nighttime Code.");
    }
}
