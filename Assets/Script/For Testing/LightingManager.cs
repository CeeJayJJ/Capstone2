using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    // Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingsPreset Preset;

    // Variables
    [SerializeField, Range(4, 24)] private float TimeOfDay = 6f; // Start at 6 AM
    [SerializeField] private float maxIntensity = 1.5f;
    private float baseIntensity = 0f;
    [SerializeField] private float maxShadowStrength = 1f;
    [SerializeField] private float minShadowStrength = 0.2f;
    private float dawn = 6f;              // 6 AM
    private float dusk = 18f;             // 6 PM
    private float noon = 12f;             // 12 PM

    // Time speed variables
    [SerializeField] private float dayDurationInMinutes = 3f;  // Duration of daytime in minutes
    [SerializeField] private float nightDurationInMinutes = 3f; // Duration of nighttime in minutes
    private float daySpeedMultiplier;  // Speed for daytime progression
    private float nightSpeedMultiplier; // Speed for nighttime progression

    private void Start()
    {
        // Calculate speed multipliers for day and night
        daySpeedMultiplier = 12f / (dayDurationInMinutes * 60f);   // 12 game hours (6 AM to 6 PM)
        nightSpeedMultiplier = 12f / (nightDurationInMinutes * 60f); // 12 game hours (6 PM to 6 AM)

        baseIntensity = maxIntensity / 2f; // Set default light intensity
        UpdateLighting(TimeOfDay / 24f);  // Initialize correct lighting
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            // Adjust time based on current day or night period
            if (TimeOfDay >= dawn && TimeOfDay < dusk) // Daytime (6 AM to 6 PM)
            {
                TimeOfDay += Time.deltaTime * daySpeedMultiplier;
            }
            else // Nighttime (6 PM to 6 AM)
            {
                TimeOfDay += Time.deltaTime * nightSpeedMultiplier;
            }

            // Adjust light intensity and shadow softness based on time of day
            if (TimeOfDay >= dawn && TimeOfDay <= noon)
            {
                DirectionalLight.intensity = baseIntensity + (baseIntensity / (noon - dawn)) * (TimeOfDay - dawn);
                DirectionalLight.shadowStrength = minShadowStrength + ((maxShadowStrength - minShadowStrength) / (noon - dawn)) * (TimeOfDay - dawn);
            }
            else if (TimeOfDay > noon && TimeOfDay <= dusk)
            {
                DirectionalLight.intensity = baseIntensity + (baseIntensity / (dusk - noon)) * (dusk - TimeOfDay);
                DirectionalLight.shadowStrength = minShadowStrength + ((maxShadowStrength - minShadowStrength) / (dusk - noon)) * (dusk - TimeOfDay);
            }
            else
            {
                DirectionalLight.intensity = baseIntensity;
                DirectionalLight.shadowStrength = minShadowStrength;
            }

            // Ensure TimeOfDay wraps back to 4 AM after reaching 24
            if (TimeOfDay >= 24f)
            {
                TimeOfDay = 4f;
            }

            UpdateLighting(TimeOfDay / 24f);
        }
    }
}
