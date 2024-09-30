using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; } // Singleton

    public float timeOfDay; // Current time of day (0-24)
    public float timeScale = 0.01f; // Adjusts the speed of time passage

    // Day/Night settings
    public float dawnTime = 6f;   // When day starts
    public float duskTime = 18f;  // When night starts
    private bool isDayTime;

    // Event that triggers when the time changes from day to night or vice versa
    public event Action<bool> OnDayNightChange;

    private void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize to day or night based on timeOfDay
        isDayTime = timeOfDay >= dawnTime && timeOfDay < duskTime;
    }

    private void Update()
    {
        UpdateTime();
        CheckDayNightTransition();
    }

    private void UpdateTime()
    {
        timeOfDay += Time.deltaTime * timeScale;

        if (timeOfDay >= 24f)
        {
            timeOfDay = 0f;
            // Trigger new day events (e.g., reset daily tasks, update quest timers)
            Debug.Log("A new day has started!");
        }

        // Update UI or other systems based on timeOfDay
    }

    private void CheckDayNightTransition()
    {
        bool currentDayTime = timeOfDay >= dawnTime && timeOfDay < duskTime;

        // Trigger event if the time transitions between day and night
        if (currentDayTime != isDayTime)
        {
            isDayTime = currentDayTime;
            OnDayNightChange?.Invoke(isDayTime);
            Debug.Log(isDayTime ? "It's now day!" : "It's now night!");
        }
    }

    // Check if it is currently day
    public bool IsDay()
    {
        return isDayTime;
    }

    // Check if it is currently night
    public bool IsNight()
    {
        return !isDayTime;
    }

    // Function to check if a quest is available based on the time of day
    public bool IsQuestAvailableAtCurrentTime(QuestData quest)
    {
        if (quest.availableAtNight && IsNight())
        {
            return true;
        }

        if (quest.availableDuringDay && IsDay())
        {
            return true;
        }

        return false; // Quest is not available if it's not the right time
    }
}