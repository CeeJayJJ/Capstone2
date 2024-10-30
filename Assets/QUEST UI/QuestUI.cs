using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public GameObject questDisplayPrefab; // Prefab for displaying a quest (assign in Unity)
    public Transform questListContainer; // Container for the quest UI elements (assign in Unity)

    public void DisplayQuests(List<QuestData> quests)
    {
        foreach (Transform child in questListContainer) // Clear existing quest displays
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in quests)
        {
            GameObject questDisplay = Instantiate(questDisplayPrefab, questListContainer);
            QuestDisplayUI questDisplayUI = questDisplay.GetComponent<QuestDisplayUI>();
            questDisplayUI.SetQuestInfo(quest);
        }
    }
}
