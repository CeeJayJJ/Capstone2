using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Quest;

public class QuestsManager : MonoBehaviour
{
    public static QuestsManager questsManager;

    public List <Quest> questList = new List <Quest> ();
    public List<Quest> currentQuestList = new List<Quest>();

    void Awake()
    {
        if (questsManager == null)
        {
            questsManager = this;
        }
        else if (questsManager != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);  
    }

    public void QuestRequest(QuestObject NPCQuestObject)
    {
        UpdateAvailableQuests(NPCQuestObject);
        UpdateActiveQuests(NPCQuestObject);
    }

    private void UpdateAvailableQuests(QuestObject NPCQuestObject)
    {
        foreach (var quest in questList)
        {
            if (quest.progress == QuestProgress.AVAILABLE && NPCQuestObject.availaQuestIDs.Contains(quest.id))
            {
                Debug.Log($"Quest {quest.id} is available");
                QuestUIManager.uiManager.questAvailable = true;
                QuestUIManager.uiManager.availableQuests.Add(quest);
            }
        }
    }

    private void UpdateActiveQuests(QuestObject NPCQuestObject)
    {
        foreach (var quest in currentQuestList)
        {
            if ((quest.progress == QuestProgress.ACCEPTED || quest.progress == QuestProgress.COMPLETE) && NPCQuestObject.receivableQuestIDs.Contains(quest.id))
            {
                Debug.Log($"Quest {quest.id} is active");
                QuestUIManager.uiManager.questRunning = true;
                QuestUIManager.uiManager.activeQuests.Add(quest);
            }
        }
    }


    public void AcceptQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPTED;
            }
        }
    }

    public void GiveUpQuest(int questID)
    {
        for (int i = 0;i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
                currentQuestList[i].questObjectiveCount = 0;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
    }

    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                currentQuestList.Remove(currentQuestList[i]);


            }
        }
        CheckChainQuest(questID);
    }

    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }
        
        if(tempID > 0)
        {
            for(int i = 0;i < questList.Count; i++)
            {
                if (questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.NOT_AVAILABLE)
                {
                    questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }
    }

    public void AddQuestItem(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].questObjective == questObjective && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }

            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }



    public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }


    public bool CheckAvailableQuest(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.availaQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.availaQuestIDs[j] && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuest(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuest(QuestObject NPCQuestObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < NPCQuestObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == NPCQuestObject.receivableQuestIDs[j] && questList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void ShowQuestLog(int questID)
    {
        for(int i =0; i < questList.Count; i++)
        {
            if (currentQuestList[i].id == questID)
            {
                QuestUIManager.uiManager.ShowQuestLog(currentQuestList[i]);
            }
        }
    }

}
