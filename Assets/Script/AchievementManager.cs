using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    // Dictionary to store achievements and their completion status
    private Dictionary<string, bool> achievements = new Dictionary<string, bool>();

    private void Awake()
    {
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

        InitializeAchievements();

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.RegisterQuestObserver(this);
        }
        else
        {
            Debug.LogError("QuestManager instance is not initialized.");
        }
    }

    private void InitializeAchievements()
    {
        GameData gameData = new GameData();
        SaveLoadManager.Instance.LoadAchievements(gameData); // Load achievements into gameData

        // If loaded achievements are available, initialize from them
        if (gameData.activeAchievements != null && gameData.activeAchievements.Any())
        {
            achievements = gameData.activeAchievements.ToDictionary(a => a.key, a => a.isUnlocked);
        }
        else
        {
            // Add default achievements if no achievements were loaded
            achievements.Add("FirstQuest_Completed", false);
            achievements.Add("DefeatDragon_Completed", false);
        }
    }

    public void OnQuestUpdated(QuestData questData, QuestEventType eventType)
    {
        if (eventType == QuestEventType.Completed)
        {
            string achievementKey = questData.questTitle + "_Completed";

            if (achievements.ContainsKey(achievementKey) && !achievements[achievementKey])
            {
                achievements[achievementKey] = true;
                TriggerAchievementNotification(achievementKey);
                SaveAchievements(); // Save immediately upon unlocking an achievement
            }
        }
    }

    public Dictionary<string, bool> GetAchievementsData()
    {
        return achievements;
    }

    public void LoadAchievementsData(Dictionary<string, bool> loadedAchievements)
    {
        achievements = loadedAchievements;
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

    private void TriggerAchievementNotification(string achievementKey)
    {
        Debug.Log("Display notification for achievement: " + achievementKey);
    }

    public void SaveAchievements()
    {
        var achievementsList = achievements
            .Select(entry => new Achievement { key = entry.Key, isUnlocked = entry.Value })
            .ToList();

        // Save achievements data within GameData
        GameData gameData = new GameData { activeAchievements = achievementsList };
        SaveLoadManager.Instance.SaveAchievements(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveAchievements();
    }

    [System.Serializable]
    public class Achievement
    {
        public string key;
        public bool isUnlocked;
    }
}

