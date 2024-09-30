using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    // Dictionary to store achievements and their completion status
    private Dictionary<string, bool> achievements = new Dictionary<string, bool>();

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
            return;
        }

        // Initialize achievements (e.g., from a save file or new game)
        InitializeAchievements();

        // Register with QuestManager to receive quest completion events
        QuestManager.Instance.RegisterQuestObserver(this);
    }

    // Initializes achievements, either by loading or by creating a default set for a new game
    private void InitializeAchievements()
    {
        // Load achievements from save file or create new entries if not found
        if (SaveLoadManager.FileExists("Achievements")) // Assuming SaveSystem is implemented
        {
            achievements = SaveLoadManager.Load<Dictionary<string, bool>>("Achievements");
        }
        else
        {
            // Example: Initial achievements for new game
            achievements.Add("FirstQuest_Completed", false);
            achievements.Add("DefeatDragon_Completed", false);
            // Add more default achievements as needed
        }
    }

    // Implement IQuestObserver interface (observer pattern)
    public void OnQuestUpdated(QuestData questData, QuestEventType eventType)
    {
        if (eventType == QuestEventType.Completed)
        {
            // Check if an achievement is associated with this quest
            string achievementKey = questData.questTitle + "_Completed";

            if (achievements.ContainsKey(achievementKey))
            {
                if (!achievements[achievementKey]) // If not already completed
                {
                    achievements[achievementKey] = true;
                    // Optionally, trigger UI notification or effects
                    Debug.Log("Achievement unlocked: " + achievementKey);

                    // Trigger notification to player (add your own method for UI feedback)
                    TriggerAchievementNotification(achievementKey);
                }
            }
        }
    }

    public Dictionary<string, bool> GetAchievementsData()
    {
        return achievements; // Return the current achievements dictionary
    }

    public void LoadAchievementsData(Dictionary<string, bool> loadedAchievements)
    {
        achievements = loadedAchievements;
    }

    // Method to check if a specific achievement is unlocked
    public bool IsAchievementUnlocked(string achievementKey)
    {
        if (achievements.ContainsKey(achievementKey))
        {
            return achievements[achievementKey];
        }
        else
        {
            Debug.LogWarning("Achievement not found: " + achievementKey);
            return false;
        }
    }

    // Optional: Method to trigger UI notifications when achievements are unlocked
    private void TriggerAchievementNotification(string achievementKey)
    {
        // Implement UI notifications or visual effects when an achievement is unlocked
        Debug.Log("Display notification for achievement: " + achievementKey);
    }

    // Save achievements data (e.g., at checkpoints or game exits)
    public void SaveAchievements()
    {
        SaveLoadManager.Save(achievements, "Achievements");
    }

    // Load achievements data (called during initialization)
    public void LoadAchievements()
    {
        if (SaveLoadManager.FileExists("Achievements"))
        {
            achievements = SaveLoadManager.Load<Dictionary<string, bool>>("Achievements");
        }
    }


}
