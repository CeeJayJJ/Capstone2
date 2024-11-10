using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using TMPro;
=======
<<<<<<< HEAD

=======
using TMPro;
>>>>>>> a774f099384fe74d0fb25d3aea80a634d4c8e43e
>>>>>>> Stashed changes
public class QLogButtonScript : MonoBehaviour
{
    public int questID;
    public TextMeshProUGUI questTitle;
<<<<<<< Updated upstream
 
=======
<<<<<<< HEAD

=======
 
>>>>>>> a774f099384fe74d0fb25d3aea80a634d4c8e43e
>>>>>>> Stashed changes
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
