using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public DayCycleData lightingData;

    public float dayDuration = 10f;
    public float nightDuration = 10f;

    private LightingHandler lh = new LightingHandler();
    private float cycleClock = 0f;
    private float currentDuration = 0f;
    bool isDayTime = true;

    void Start()
    {
        if (lightingData.lightPivot == null || lightingData.light == null)
        {
            Debug.LogError("Lighting data is missing Light Pivot or Light component!");
            enabled = false;
            return;
        }

        lh.SetLightingData(lightingData);
   
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

        if (cycleClock >= currentDuration)
        {
            ChangeDayState();
        }
    }

    private void UpdateLighting()
    {
        float cycleProgression = cycleClock / currentDuration;
        
        lh.UpdateLighting(cycleProgression, isDayTime);
    }

    private void StartDay()
    {
        isDayTime = true;
        currentDuration = dayDuration;
        cycleClock = 0f;

        Debug.Log("Starting Day");
    }
    private void StartNight()
    {
        // Code to Activate Night
        isDayTime = false;
        currentDuration = nightDuration;
        cycleClock = 0f;

        Debug.Log("Starting Night");
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
    }
}
