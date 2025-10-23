using UnityEngine;

public class LightingHandler
{
    private DayCycleData lightingData;

    private float middayAngle;
    private float midnightAngle;

    public void SetLightingData(DayCycleData data)
    {
        lightingData = data;
        (middayAngle, midnightAngle) = CalculateMidAngles(lightingData.dayStartAngle, lightingData.nightStartAngle);
    }

    public void UpdateLighting(float cycleProgression, bool isDay)
    {
        Vector3 currentEuler = lightingData.lightPivot.localEulerAngles;

        // Update Light Rotation
        float dayStart = lightingData.dayStartAngle;
        float nightStart = lightingData.nightStartAngle;
        float midday = middayAngle;
        float midnight = midnightAngle;

        float startAngle;
        float endAngle;
        float lerpT;

        if (cycleProgression <= 0.25f) // Q1: Sunrise to Midday
        {
            startAngle = dayStart;
            endAngle = midday;
            lerpT = cycleProgression * 4f; // Remap 0.0-0.25 to 0.0-1.0
        }
        else if (cycleProgression <= 0.50f) // Q2: Midday to Sunset
        {
            startAngle = midday;
            endAngle = nightStart;
            lerpT = (cycleProgression - 0.25f) * 4f; // Remap 0.25-0.50 to 0.0-1.0
        }
        else if (cycleProgression <= 0.75f) // Q3: Sunset to Midnight
        {
            startAngle = nightStart;
            endAngle = midnight;
            lerpT = (cycleProgression - 0.50f) * 4f; // Remap 0.50-0.75 to 0.0-1.0
        }
        else // Q4: Midnight to Sunrise (End of Cycle)
        {
            startAngle = midnight;
            // The crucial fix: ensure a full rotation back to the start angle
            endAngle = dayStart + 360f;
            lerpT = (cycleProgression - 0.75f) * 4f; // Remap 0.75-1.0 to 0.0-1.0
        }

        Quaternion startRot = Quaternion.Euler(currentEuler.x, currentEuler.y, startAngle);
        Quaternion endRot = Quaternion.Euler(currentEuler.x, currentEuler.y, endAngle);

        lightingData.lightPivot.localRotation = Quaternion.Slerp(startRot, endRot, lerpT);

        // Update Light Color and Intensity
        Color startColor = new Color();
        Color endColor = new Color();

        float startIntensity = 0f;
        float endIntensity = 0f;

        if (cycleProgression <= 0.25f)
        {
            startColor = lightingData.sunriseColor;
            endColor = lightingData.daylightColor;
            startIntensity = lightingData.sunriseIntensity;
            endIntensity = lightingData.dayIntensity;
        }
        // Q2: 0.25 to 0.50 (Midday to Sunset)
        else if (cycleProgression <= 0.50f)
        {
            startColor = lightingData.daylightColor;
            endColor = lightingData.sunsetColor;
            startIntensity = lightingData.dayIntensity;
            endIntensity = lightingData.sunsetIntensity;
        }
        // Q3: 0.50 to 0.75 (Sunset to Midnight)
        else if (cycleProgression <= 0.75f)
        {
            startColor = lightingData.sunsetColor;
            endColor = lightingData.daylightColor;
            startIntensity = lightingData.sunsetIntensity;
            endIntensity = lightingData.dayIntensity;
        }
        // Q4: 0.75 to 1.00 (Midnight to Sunrise)
        else
        {
            startColor = lightingData.nightColor;
            endColor = lightingData.sunriseColor;
            startIntensity = lightingData.nightIntensity;
            endIntensity = lightingData.sunriseIntensity;
        }

        lightingData.light.color = Color.Lerp(startColor, endColor, lerpT);
        lightingData.light.intensity = Mathf.Lerp(startIntensity, endIntensity, lerpT);
    }

    public (float, float) CalculateMidAngles(float dayStart, float nightStart)
    {
        float dayShift = nightStart - dayStart;

        if (dayShift < 0)
        {
            dayShift += 360f;
        }

        float middayAngle = (dayStart + (dayShift / 2f)) % 360f;

        float nightShift = 360f - dayShift;

        float midnightAngle = (nightStart + (nightShift / 2f)) % 360f;

        return (middayAngle, midnightAngle);
    }
}
