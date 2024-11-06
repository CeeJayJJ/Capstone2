using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QButtonScript : MonoBehaviour
{
    public int questID;
    public TMPro.TextMeshProUGUI questTitle;
    private GameObject acceptButton;
    private GameObject giveUpButton;
    private GameObject completeButton;

    private QButtonScript acceptButtonScript;
    private QButtonScript giveUpButtonScript;
    private QButtonScript completeButtonScript;


    private void Start()
    {
        // First, find the button GameObjects
        acceptButton = GameObject.Find("Canvas/Quest Log Panel1/Quest Panel/QuestDescription/GameObject/AcceptButton");
        giveUpButton = GameObject.Find("Canvas/Quest Log Panel1/Quest Panel/QuestDescription/GameObject/GiveupButton");
        completeButton = GameObject.Find("Canvas/Quest Log Panel1/Quest Panel/QuestDescription/GameObject/CompleteButton");

        // Now check if each button was found and get the QButtonScript components
        if (acceptButton != null)
        {
            acceptButtonScript = acceptButton.GetComponent<QButtonScript>();
            acceptButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("AcceptButton is missing in the scene!");
        }

        if (giveUpButton != null)
        {
            giveUpButtonScript = giveUpButton.GetComponent<QButtonScript>();
            giveUpButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GiveUpButton is missing in the scene!");
        }

        if (completeButton != null)
        {
            completeButtonScript = completeButton.GetComponent<QButtonScript>();
            completeButton.SetActive(false);
        }
        else
        {
            Debug.LogWarning("CompleteButton is missing in the scene!");
        }
    }

    public void ShowAllInfos()
    {
        // Display quest information
        QuestUIManager.uiManager.ShowSelectedQuest(questID);

        // Configure each button's visibility and quest ID
        ConfigureButton(acceptButton, acceptButtonScript, QuestsManager.questsManager.RequestAvailableQuest(questID), questID);
        ConfigureButton(giveUpButton, giveUpButtonScript, QuestsManager.questsManager.RequestAcceptedQuest(questID), questID);
        ConfigureButton(completeButton, completeButtonScript, QuestsManager.questsManager.RequestCompleteQuest(questID), questID);
    }

    // Helper method to configure button visibility and quest ID
    private void ConfigureButton(GameObject button, QButtonScript buttonScript, bool condition, int questID)
    {
        if (button != null && buttonScript != null)
        {
            button.SetActive(condition);
            if (condition)
            {
                buttonScript.questID = questID;
            }
        }
        else
        {
            Debug.LogWarning("Button or Button Script is missing!");
        }
    }

    public void AcceptQuest()
    {
        QuestsManager.questsManager.AcceptQuest(questID);
        QuestUIManager.uiManager.HideQuestPanel();
    }
}