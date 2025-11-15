using UnityEngine;

public class LightingHandler
{
    private DayCycleData lightingData;
    private DayCycleProgressionStages stages;

    private float middayAngle;
    private float midnightAngle;

    public void SetLightingData(DayCycleData data, DayCycleProgressionStages stages)
    {
        lightingData = data;
        this.stages = stages;

        // swapping Assignment because calculation gets them backwards
        (midnightAngle, middayAngle) = CalculateMidAngles(lightingData.dayStartAngle, lightingData.nightStartAngle);
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

        Color startColor = new Color();
        Color endColor = new Color();

        float startIntensity = 0f;
        float endIntensity = 0f;

        float startShadowStrength = 0f;
        float endShadowStrength = 0f;

        if (cycleProgression <= stages.morningStart)
        {
            startAngle = dayStart;
            endAngle = midday;

            startColor = lightingData.sunriseColor;
            endColor = lightingData.daylightColor;
            startIntensity = lightingData.sunriseIntensity;
            endIntensity = lightingData.dayIntensity;

            startShadowStrength = lightingData.sunriseShadow;
            endShadowStrength = lightingData.dayShadow;

            lerpT = cycleProgression / stages.morningStart;
        }
        else if (cycleProgression <= stages.NoonStart)
        {
            startAngle = midday;
            endAngle = nightStart;

            startColor = lightingData.daylightColor;
            endColor = lightingData.sunsetColor;
            startIntensity = lightingData.dayIntensity;
            endIntensity = lightingData.sunsetIntensity;

            startShadowStrength = lightingData.dayShadow;
            endShadowStrength = lightingData.sunsetShadow;

            float dayDuration = stages.NoonStart - stages.morningStart;

            lerpT = (cycleProgression - stages.morningStart) / dayDuration;
        }
        else if (cycleProgression <= stages.NightStart)
        {
            startAngle = nightStart;
            endAngle = midnight;

            startColor = lightingData.sunsetColor;
            endColor = lightingData.daylightColor;
            startIntensity = lightingData.sunsetIntensity;
            endIntensity = lightingData.nightIntensity;

            startShadowStrength = lightingData.sunsetShadow;
            endShadowStrength = lightingData.nightShadow;
            
            float eveningDuration = stages.NightStart - stages.NoonStart;
            lerpT = (cycleProgression - stages.NoonStart) / eveningDuration;
        }
        else
        {
            startAngle = midnight;
            endAngle = dayStart + 360f;

            startColor = lightingData.nightColor;
            endColor = lightingData.sunriseColor;
            startIntensity = lightingData.nightIntensity;
            endIntensity = lightingData.sunriseIntensity;

            startShadowStrength = lightingData.nightShadow;
            endShadowStrength = lightingData.sunriseShadow;
            
            float nightDuration = 1f - stages.NightStart;
            lerpT = (cycleProgression - stages.NightStart) / nightDuration;
        }

        Quaternion startRot = Quaternion.Euler(currentEuler.x, currentEuler.y, startAngle);
        Quaternion endRot = Quaternion.Euler(currentEuler.x, currentEuler.y, endAngle);

        lightingData.lightPivot.localRotation = Quaternion.Slerp(startRot, endRot, lerpT);
        lightingData.light.color = Color.Lerp(startColor, endColor, lerpT);
        lightingData.light.intensity = Mathf.Lerp(startIntensity, endIntensity, lerpT);
        lightingData.light.shadowStrength = Mathf.Lerp(startShadowStrength, endShadowStrength, lerpT);
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
