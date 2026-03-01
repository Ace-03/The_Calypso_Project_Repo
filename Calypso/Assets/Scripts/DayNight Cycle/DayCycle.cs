using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private OnDayStateChangeEventSO DayStateChangeEvent;
    [SerializeField] private OnDeathEventSO deathEvent;
    [SerializeField] private OnRespawnPlayerEventSO respawnEvent;

    public DayCycleData lightingData;
    public DayCycleProgressionStages progressionStages;

    public float dayDuration = 120f;
    [Range(0, 1)]public float dayLengthPercentage;

    public SpawnManager spawner;

    private LightingHandler lh = new LightingHandler();
    private float cycleClock = 0f;
    private float dayNightClock = 0f;
    private int dayCount = 0;
    bool isDayTime = true;
    private bool playerDead;

    void Start()
    {
        if (lightingData.lightPivot == null || lightingData.light == null)
        {
            Debug.LogError("Lighting data is missing Light Pivot or Light component!");
            enabled = false;
            return;
        }

        lh.SetLightingData(lightingData, progressionStages);
   
        StartDay();
    }

    void Update()
    {
        if (playerDead) return;

        UpdateClock();
        UpdateLighting();
    }
    void UpdateClock()
    {
        cycleClock += Time.deltaTime;
        dayNightClock += Time.deltaTime;

        float dayNightRatio = isDayTime ? dayDuration * dayLengthPercentage :
            dayDuration * (1 - dayLengthPercentage);

        if (dayNightClock >= (dayNightRatio))
            ChangeDayState();

        if (cycleClock >= dayDuration)
            CompleteCycle();
    }

    private void UpdateLighting()
    {
        float cycleProgression = cycleClock / dayDuration;
        
        lh.UpdateLighting(cycleProgression, isDayTime);
    }

    private void StartDay()
    {
        Debug.Log("Starting Day");

        isDayTime = true;
        dayCount++;
    }
    private void StartNight()
    {
        Debug.Log("Starting Night");

        isDayTime = false;
    }

    private void ChangeDayState()
    {
        if (isDayTime)
            StartNight();
        else
            StartDay();

        DayStateChangeEvent.Raise(new DayStateChangePayload(isDayTime, dayCount));
        dayNightClock = 0f;
    }

    private void CompleteCycle()
    {
        cycleClock = 0f;
    }

    public float GetDayProgressPercentage()
    {
        float dayNightRatio = isDayTime ? dayDuration * dayLengthPercentage :
        dayDuration * (1 - dayLengthPercentage);

        return dayNightClock / dayNightRatio;
    }

    public float GetCycleProgressProgression()
    {
        return cycleClock / dayDuration;
    }

    public void SkipCurrentDay()
    {
        StartDay();
        dayNightClock = 0;
        cycleClock = 0f;
        DayStateChangeEvent.Raise(new DayStateChangePayload(isDayTime, dayCount));
    }

    private void StopClock(DeathPayload payload) => playerDead = true;
    private void StartClock(RespawnScenePayload payload) => playerDead = false;

    private void OnEnable()
    {
        deathEvent.RegisterListener(StopClock);
        respawnEvent.RegisterListener(StartClock);
    }

    private void OnDisable()
    {
        deathEvent.UnregisterListener(StopClock);
        respawnEvent.UnregisterListener(StartClock);
    }
}

