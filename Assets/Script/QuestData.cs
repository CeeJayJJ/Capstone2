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
    public float questTimer; // Optional, use only if needed
    public string questLocation; // Or use Transform/Location if you have a map system

    // Enum for quest conditions (you can customize this further)
    public enum QuestCondition
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    // Enum for quest status
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
}
