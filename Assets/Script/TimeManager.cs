using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; } // Singleton

    public float timeOfDay; // Current time of day (0-24)
    public float timeScale = 1f; // Adjusts the speed of time passage

    private void Awake()
    {
        // Singleton implementation (similar to other core scripts)
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime * timeScale;
        if (timeOfDay >= 24f)
        {
            timeOfDay = 0f;
            // Trigger new day events (e.g., reset daily tasks, update quest timers)
        }

        // Update UI or other systems based on timeOfDay
    }
}
