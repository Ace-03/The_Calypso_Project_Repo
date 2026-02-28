using System.Collections.Generic;
using UnityEngine;

public class DayTimerUI : MonoBehaviour
{
    private DayTimerUIElements elements;
    private DayCycle dayCycle;
    private List<GameObject> timeLineWaves = new List<GameObject>();
    private int currentLineWaveCount = 0;

    private void OnEnable()
    {
        if (elements != null)
        {
            elements.dayStateChangeEvent.RegisterListener(ChangeDayState);
        }
    }
    private void OnDisable()
    {
        elements.dayStateChangeEvent.UnregisterListener(ChangeDayState);
    }
    private void Start()
    {
        dayCycle = ContextRegister.Instance.GetContext().DayCycle;
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }

    public void SetElements(DayTimerUIElements uiElements)
    {
        elements = uiElements;
        elements.dayStateChangeEvent.RegisterListener(ChangeDayState);
    }

    private void UpdateUI()
    {
        float dayProgress = dayCycle.GetDayProgressPercentage();

        int newLineWaveCount = (int)Mathf.Round(dayProgress * elements.maxTimeLineWaves);

        if (newLineWaveCount > currentLineWaveCount)
        {
            UpdateTimeLineWaves(newLineWaveCount);
            currentLineWaveCount = newLineWaveCount;
        }
    }

    private void UpdateTimeLineWaves(int newLineWaveCount)
    {
        GameObject newWave = Instantiate(elements.TimelineWavePrefab, elements.timeLineContainer);
        timeLineWaves.Add(newWave);
    }

    private void ChangeDayState(DayStateChangePayload payload)
    {
        Debug.Log("Day State Changed. Is Day Time: " + payload.isDayTime);
        if (payload.isDayTime)
        {
            elements.TimelineStartIcon.sprite = elements.sunSprite;
            elements.TimelineEndIcon.sprite = elements.moonSprite;
        }
        else
        {
            elements.TimelineStartIcon.sprite = elements.moonSprite;
            elements.TimelineEndIcon.sprite = elements.sunSprite;
        }

        foreach (GameObject wave in timeLineWaves)
        {
            Debug.Log("Destroying Wave UI Element");
            Destroy(wave);
        }
     
        timeLineWaves.Clear();
        currentLineWaveCount = 0;
    }
}
