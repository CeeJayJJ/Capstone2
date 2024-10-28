using UnityEngine;
using System.Collections.Generic;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; } // Singleton

    private List<IQuestObserver> questObservers = new List<IQuestObserver>();
    private List<QuestData> quests = new List<QuestData>();

    public PlayerData playerData; // Assign in the Unity Inspector


    private void Awake()
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
    }

    // Method to start a new quest
    public void StartQuest(QuestData quest)
    {
        if (quest != null && !quests.Contains(quest))
        {
            quests.Add(quest);
            quest.status = QuestData.QuestStatus.InProgress;
            Debug.Log("Quest started: " + quest.questTitle);
        }
        else
        {
            Debug.LogWarning("Quest is already started or null.");
        }
    }

    public void CompleteQuest(QuestData quest)
    {
        if (quest == null || !quests.Contains(quest)) return;

        quest.status = QuestData.QuestStatus.Completed;
        Debug.Log("Quest completed: " + quest.questTitle);

        foreach (var reward in quest.rewards)
        {
            switch (reward.type)
            {
                case QuestData.Reward.RewardType.Gold:
                    playerData.AddGold(reward.amount); // Assuming PlayerData has AddGold
                    break;
                case QuestData.Reward.RewardType.Item:
                    playerData.AddItem(reward.item);
                    break;
            }
        }
    }
    public bool IsQuestAvailableAtCurrentTime(QuestData quest)
    {
        if (TimeManager.Instance == null) return true;

        return (quest.availableDuringDay && TimeManager.Instance.IsDay()) ||
               (quest.availableAtNight && TimeManager.Instance.IsNight());
    }


    public void UpdateQuest(QuestData questData, QuestEventType eventType)
    {
        // ... (Logic to update quest progress)

        NotifyQuestObservers(questData, eventType);
    }

    public void RegisterQuestObserver(IQuestObserver observer)
    {
        questObservers.Add(observer);
    }

    public void UnregisterQuestObserver(IQuestObserver observer)
    {
        questObservers.Remove(observer);
    }

    private void NotifyQuestObservers(QuestData questData, QuestEventType eventType)
    {
        foreach (var observer in questObservers)
        {
            observer.OnQuestUpdated(questData, eventType);
        }
    }

    internal void RegisterQuestObserver(AchievementManager achievementManager)
    {
        throw new NotImplementedException();
    }

    // Method to get all quests for saving
    public QuestData[] GetQuestsData()
    {
        return quests.ToArray(); // Convert the list to an array for saving
    }

    // Method to load saved quests
    public void LoadQuestsData(QuestData[] loadedQuests)
    {
        quests = new List<QuestData>(loadedQuests); // Convert the loaded array back to a list
    }
}

public interface IQuestObserver
{
    void OnQuestUpdated(QuestData questData, QuestEventType eventType);
}

public enum QuestEventType
{
    Started,
    Updated,
    Completed
}


