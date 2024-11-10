using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class QLogButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;
 
    public void ShowAllInfos()
    {
        //QuestUIManager.uiManager.ShowQuestLog(questID);
        QuestsManager.questsManager.ShowQuestLog(questID);
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestLogPanel();
    }
}
