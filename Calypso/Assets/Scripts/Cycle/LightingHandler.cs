using Unity.VisualScripting;
using UnityEngine;

public class LightingHandler : MonoBehaviour
{
    private dayCycleData lightingData;



    public void SetLightingData()
    {

    }

    public void UpdateLighting(float cycleProgression, bool isDay)
    {
        float currentrotationZ = lightingData.lightPivot.rotation.z;
        float rotationRange = 0f;


        if (isDay)
        {
            rotationRange = lightingData.nightStartangle - lightingData.dayStartangle;
        }
        else
        {
            rotationRange = lightingData.dayStartangle - lightingData.nightStartangle;
        }



        float newRotationZ = Mathf.Lerp(currentrotationZ, );
        lightingData.lightPivot.rotation =
    }

    public LightingState TransitionLightState(LightingState state)
    {
        LightingState newState = state;
        Color newColor = Color.black;

        switch (state)
        {
            case LightingState.sunrise:
                newState = LightingState.morning;
                newColor = lightingData.morningColor;
                // Implement sunrise lighting changes
                break;
            case LightingState.morning:
                newState = LightingState.daylight;
                newColor = lightingData.daylightColor;
                // Implement morning lighting changes
                break;
            case LightingState.daylight:
                newState = LightingState.evening;
                newColor = lightingData.eveningColor;
                // Implement daylight lighting changes
                break;
            case LightingState.evening:
                newState = LightingState.sunset;
                newColor = lightingData.sunsetColor;
                // Implement evening lighting changes
                break;
            case LightingState.sunset:
                newState = LightingState.night;
                newColor = lightingData.nightColor;
                // Implement sunset lighting changes
                break;
            case LightingState.night:
                newState = LightingState.sunrise;
                newColor = lightingData.sunriseColor;
                // Implement night lighting changes
                break;
            default:
                Debug.LogWarning("Unknown lighting state");
                break;


        }

        lightingData.light.color = Color.Lerp(lightingData.light.color, newColor, Time.deltaTime);
        return newState;

    }
}
