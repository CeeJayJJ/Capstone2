using UnityEngine;

public class DayNightCycleManager : MonoBehaviour
{
    public Light directionalLight;      // The light representing the sun
    public Gradient ambientLightColor;  // Gradient for ambient light over the day
    public Gradient directionalLightColor;  // Gradient for sun color over the day
    public Gradient skyboxTintColor;    // Optional, if you're adjusting skybox tint over the day

    public float nightStart = 18f;      // Start of night (6 PM)
    public float nightEnd = 6f;         // End of night (6 AM)

    private void Update()
    {
        float timeOfDay = TimeManager.Instance.timeOfDay;

        // Adjust sun intensity and color based on time of day
        UpdateSunlight(timeOfDay);

        // Adjust ambient lighting and skybox tint
        RenderSettings.ambientLight = ambientLightColor.Evaluate(timeOfDay / 24f);
        RenderSettings.skybox.SetColor("_Tint", skyboxTintColor.Evaluate(timeOfDay / 24f));
    }

    private void UpdateSunlight(float timeOfDay)
    {
        if (directionalLight != null)
        {
            // Adjust light intensity based on time of day (simulate sunrise/sunset)
            if (timeOfDay > nightEnd && timeOfDay < nightStart)
            {
                directionalLight.intensity = Mathf.Lerp(0, 1, (timeOfDay - nightEnd) / (nightStart - nightEnd));
            }
            else
            {
                // Reduce light during the night
                directionalLight.intensity = Mathf.Lerp(1, 0, (timeOfDay < nightEnd ? timeOfDay : 24f - timeOfDay) / nightEnd);
            }

            // Change the sun's color based on time of day
            directionalLight.color = directionalLightColor.Evaluate(timeOfDay / 24f);
        }
    }
}
