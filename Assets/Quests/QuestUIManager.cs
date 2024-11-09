using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager uiManager;

    public bool questAvailable = false;
    public bool questRunning = false;
    private bool questPanelActive = false;
    private bool questLogPanelActive = false;

    public GameObject questPanel;
    public GameObject questLogPanel;

    private QuestObject currentQuestObject;

    public List <Quest> availableQuests = new List <Quest> ();
    public List<Quest> activeQuests = new List<Quest>();

    public GameObject qButton;
    public GameObject qLogButton;
    private List<GameObject> qButtons = new List <GameObject> ();

    public GameObject acceptButton;
    public GameObject giveUpButton;
    public GameObject completeButton;

    public GameObject acceptButton1;
    public GameObject giveUpButton1;
    public GameObject completeButton1;

    public Transform qButtonSpacer1;
    public Transform qButtonSpacer2;
    public Transform qLogButtonSpacer;

    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questSummary;

    public TextMeshProUGUI questLogTitle;
    public TextMeshProUGUI questLogDescription;
    public TextMeshProUGUI questLogSummary;

    public QButtonScript acceptButtonScript;
    public QButtonScript giveUpButtonScript;
    public QButtonScript completeButtonScript;

    public QButtonScript acceptButtonScript1;
    public QButtonScript giveUpButtonScript1;
    public QButtonScript completeButtonScript1;


    private void Start()
    {
       // Use FindWithTag to locate each button by its tag
       // acceptButton = GameObject.FindWithTag("Accept");
       // giveUpButton = GameObject.FindWithTag("GiveUp");
       // completeButton = GameObject.FindWithTag("Complete");

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

        // Now check if each button was found and get the QButtonScript components
        if (acceptButton1 != null)
        {
            acceptButtonScript1 = acceptButton1.GetComponent<QButtonScript>();
            acceptButton1.SetActive(false);
        }
        else
        {
            Debug.LogWarning("AcceptButton is missing in the scene!");
        }

        if (giveUpButton1 != null)
        {
            giveUpButtonScript1 = giveUpButton1.GetComponent<QButtonScript>();
            giveUpButton1.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GiveUpButton is missing in the scene!");
        }

        if (completeButton1 != null)
        {
            completeButtonScript1 = completeButton1.GetComponent<QButtonScript>();
            completeButton1.SetActive(false);
        }
        else
        {
            Debug.LogWarning("CompleteButton is missing in the scene!");
        }
    }



    private void Awake()
    {
        if(uiManager == null)
        {
            uiManager = this;
        }
        else if (uiManager != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        HideQuestPanel();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            questLogPanelActive = !questLogPanelActive;
            ShowQuestLogPanel();
        }
    }

    public void CheckQuest(QuestObject questObject)
    {
        currentQuestObject = questObject;
        QuestsManager.questsManager.QuestRequest(questObject);

        if((questRunning || questAvailable) && !questPanelActive)
        {
            ShowQuestPanel();
        }
        else
        {
            Debug.Log("No Quest available!");
        }
    }

    public void ShowQuestPanel()
    {
        questPanelActive = true;
        questPanel.SetActive(questPanelActive);

        FillQuestButtons();
    }

    public void ShowQuestLogPanel()
    {
        questLogPanel.SetActive(questLogPanelActive);
        if (questLogPanelActive && !questPanelActive)
        {
            foreach (Quest curQuest in QuestsManager.questsManager.currentQuestList)
            {
                GameObject questButton = Instantiate(qLogButton);
                QLogButtonScript qbutton = questButton.GetComponent<QLogButtonScript>();

                qbutton.questID = curQuest.id;
                qbutton.questTitle.text = curQuest.title;

                questButton.transform.SetParent(qLogButtonSpacer, false);
                qButtons.Add(questButton);
            }
        }
        else if (!questLogPanelActive && !questPanelActive)
        {
            HideQuestLogPanel();
        }
    }

    public void ShowQuestLog(Quest activeQuest)
    {
        questLogTitle.text = activeQuest.title;
        if (activeQuest.progress == Quest.QuestProgress.ACCEPTED)
        {
            questLogDescription.text = activeQuest.hint;
            questLogSummary.text = activeQuest.questObjective + " : " +activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }
        else if (activeQuest.progress == Quest.QuestProgress.COMPLETE)
        {
            questLogDescription.text = activeQuest.congratulation;
            questLogSummary.text = activeQuest.questObjective + " : " + activeQuest.questObjectiveCount + " / " + activeQuest.questObjectiveRequirement;
        }

    }
    public void HideQuestPanel()
    {
        questPanelActive = false;
        questAvailable = false;
        questRunning = false;

        questTitle.text = "";
        questDescription.text = "";
        questSummary.text = "";

        availableQuests.Clear();
        activeQuests.Clear();

        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]);
        }

        qButtons.Clear();
        questPanel.SetActive(questPanelActive);
    }

    public void HideQuestLogPanel()
    {
        questLogPanelActive = false;

        questLogTitle.text = "";
        questLogDescription.text = "";
        questLogSummary.text = "";

        for (int i = 0; i < qButtons.Count; i++)
        {
            Destroy(qButtons[i]) ;
        }

        qButtons.Clear();
        questLogPanel.SetActive(questLogPanelActive);
    }
    void FillQuestButtons()
    {
        foreach (Quest availableQuest in availableQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = availableQuest.id;
            qBScript.questTitle.text = availableQuest.title;

            questButton.transform.SetParent(qButtonSpacer1, false);
            qButtons.Add(questButton);
        }

        foreach (Quest activeQuest in activeQuests)
        {
            GameObject questButton = Instantiate(qButton);
            QButtonScript qBScript = questButton.GetComponent<QButtonScript>();

            qBScript.questID = activeQuest.id;
            qBScript.questTitle.text = activeQuest.title;

            questButton.transform.SetParent(qButtonSpacer2, false);
            qButtons.Add(questButton);
        }
    }

    public void ShowSelectedQuest(int questID)
    {
        for (int i = 0; i < availableQuests.Count; i++)
        {
            if (availableQuests[i].id == questID)
            {
                questTitle.text = availableQuests[i].title;
                if (availableQuests[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    questDescription.text = availableQuests[i].description;
                    questSummary.text = availableQuests[i].questObjective + " : " + availableQuests[i].questObjectiveCount + "/" + availableQuests[i].questObjectiveRequirement;
                }
            }
        }

        for(int i = 0;i < activeQuests.Count; i++)
        {
            if (activeQuests[i].id == questID)
            {
                questTitle.text = activeQuests[i].title;
                if (activeQuests[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    questDescription.text = activeQuests[i].hint;
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " +activeQuests[i].questObjectiveRequirement;
                }
                else if(activeQuests[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    questDescription.text = activeQuests[i].congratulation;
                    questSummary.text = activeQuests[i].questObjective + " : " + activeQuests[i].questObjectiveCount + " / " + activeQuests[i].questObjectiveRequirement;
                }
            }
        }
    }
}
