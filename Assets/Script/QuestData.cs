using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "RPG/Quest")]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string questDescription;
    public QuestCondition condition = QuestCondition.NotStarted;
    public Reward[] rewards;
    public QuestStatus status = QuestStatus.NotStarted;
    public Sprite icon;
    public float questTimer; // Optional timer for quest (e.g., time-limited quests)
    public string questLocation; // Location for the quest, can use Transform if linked to a map system

    // ** New Time Availability Fields **
    public bool availableDuringDay = true;   // Whether the quest is available during the day
    public bool availableAtNight = false;    // Whether the quest is available at night

    // ** Optional Time Limit (e.g., must complete by a certain time of day) **
    public bool hasTimeLimit = false;  // If true, the quest must be completed by a specific time of day
    public float completionDeadline;   // Specific time of day when quest must be completed (e.g., 18.0 = 6:00 PM)

    // New fields for time-based availability
    public bool isTimeLimited;
    public float startHour;  // Start time for the quest (e.g., 6 for 6 AM)
    public float endHour;    // End time for the quest (e.g., 18 for 6 PM)
    // Enums for quest conditions

    public bool IsQuestAvailable()
    {
        if (isTimeLimited)
        {
            float timeOfDay = TimeManager.Instance.timeOfDay;
            return timeOfDay >= startHour && timeOfDay <= endHour;
        }
        return true; // If not time-limited, always available
    }

    public enum QuestCondition
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    // Enums for quest status
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    // Struct to represent different reward types
    [System.Serializable]
    public struct Reward
    {
        public enum RewardType { Gold, Experience, Item }
        public RewardType type;
        public int amount; // For gold or experience
        public ItemData item; // Reference to an ItemData ScriptableObject (if you have one)
    }

    // Optional: Check if the quest is available at the current time based on day/night cycle
    public bool IsQuestAvailableAtCurrentTime()
    {
        // Check availability based on time of day from the TimeManager
        if (TimeManager.Instance != null)
        {
            if (availableDuringDay && TimeManager.Instance.IsDay())
            {
                return true;
            }
            if (availableAtNight && TimeManager.Instance.IsNight())
            {
                return true;
            }
        }
        return false;
    }

    // Optional: Check if the quest has a time limit and if the current time is within the deadline
    public bool IsWithinTimeLimit()
    {
        if (hasTimeLimit && TimeManager.Instance != null)
        {
            return TimeManager.Instance.timeOfDay <= completionDeadline;
        }
        return true; // If no time limit, always return true
    }
}
