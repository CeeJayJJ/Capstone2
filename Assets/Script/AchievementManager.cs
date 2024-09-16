using UnityEngine;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    // Dictionary to store achievements and their completion status
    private Dictionary<string, bool> achievements = new Dictionary<string, bool>();

    void Awake()
    {
        // Singleton implementation (similar to other core scripts)
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

        // Initialize achievements (load from save file or set all to false if new game)
        // ...

        // Register with QuestManager to receive quest completion events
        QuestManager.Instance.RegisterQuestObserver(this);
    }

    // Implement IQuestObserver interface
    public void OnQuestUpdated(QuestData questData, QuestEventType eventType)
    {
        if (eventType == QuestEventType.Completed)
        {
            // Check if there's an achievement associated with this quest
            string achievementKey = questData.questName + "_Completed"; // Example key

            if (achievements.ContainsKey(achievementKey))
            {
                achievements[achievementKey] = true;
                // Optionally, trigger UI notification or other effects
                Debug.Log("Achievement unlocked: " + achievementKey);
            }
        }
    }

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

    // ... (Other achievement management methods as needed)
}