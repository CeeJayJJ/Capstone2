using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QLogButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;


    public void ShowAllInfos()
    {
        QuestsManager.questsManager.ShowQuestLog(questID);
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManager.HideQuestLogPanel();
    }
}
