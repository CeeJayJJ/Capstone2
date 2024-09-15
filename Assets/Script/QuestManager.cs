using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; } // Singleton

    private List<IQuestObserver> questObservers = new List<IQuestObserver>();

    void Awake()
    {
        // Singleton implementation (similar to PlayerController)
    }

    public void StartQuest(QuestData questData)
    {
        // ... (Logic to start a quest)

        NotifyQuestObservers(questData, QuestEventType.Started);
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

