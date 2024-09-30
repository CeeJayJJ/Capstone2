using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    // UI elements (assign these in the Inspector)
    public NPCData npcData;
    public GameObject dialoguePanel;
    public TextMesh npcNameText;
    public Image npcPortraitImage;
    public TextMesh dialogueText;
    public Button choice1Button, choice2Button;

    public delegate void DialogueEndedEventHandler(DialogueData dialogueData, int selectedChoice, int lineIndex);
    public event DialogueEndedEventHandler OnDialogueEnded;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    public int lastSelectedChoice { get; private set; } // Store the last selected choice

    private void Awake()
    {
        // Singleton implementation
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

        // Initially hide the dialogue panel
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        currentDialogue = dialogueData;
        currentLineIndex = 0;
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (currentDialogue == null)
        {
            Debug.LogError("No dialogue data assigned!");
            return;
        }

        if (currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            // Display the current dialogue line
            dialoguePanel.SetActive(true);
            npcNameText.text = currentDialogue.npcName;
            npcPortraitImage.sprite = currentDialogue.npcPortrait;
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];

            // Check if there's a choice at this line
            if (currentLineIndex < currentDialogue.choices.Count)
            {
                // Display choices only if they exist
                var currentChoice = currentDialogue.choices[currentLineIndex];

                choice1Button.gameObject.SetActive(true);
                choice2Button.gameObject.SetActive(true);

                choice1Button.GetComponentInChildren<Text>().text = currentChoice.choiceText;
                choice2Button.GetComponentInChildren<Text>().text = currentChoice.choiceText2;
            }
            else
            {
                // No more choices, hide choice buttons
                choice1Button.gameObject.SetActive(false);
                choice2Button.gameObject.SetActive(false);
            }
        }
        else
        {
            // End of dialogue
            EndDialogue();
        }
    }


    public void OnChoiceSelected(int choiceIndex)
    {
        lastSelectedChoice = choiceIndex;

        if (choiceIndex == 1)
        {
            currentLineIndex = currentDialogue.choices[currentLineIndex].nextDialogueIndexIfChosen;
        }
        else if (choiceIndex == 2)
        {
            currentLineIndex = currentDialogue.choices[currentLineIndex].nextDialogueIndexIfChosen2;
        }

        DisplayNextLine();
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        // Trigger the event, passing the dialogue data and selected choice
        OnDialogueEnded?.Invoke(currentDialogue, lastSelectedChoice, currentLineIndex);

        currentDialogue = null;
        currentLineIndex = 0;
    }

    private void Interact()
    {
        // Update relationship status
        npcData.relationshipStatus += CalculateRelationshipChange();

        // Trigger dialogue or other interactions
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(npcData.GetDialogueBasedOnRelationship());

            // Listen for dialogue end event
            DialogueManager.Instance.OnDialogueEnded += HandleDialogueEnd;
        }
        else
        {
            Debug.LogError("DialogueManager instance is not found!");
        }
    }



    private int CalculateRelationshipChange()
    {
        // Simple example: Increase relationship by 10
        return 10;
    }





    private void HandleDialogueEnd(DialogueData dialogueData, int selectedChoice, int lineIndex)
    {
        // Unsubscribe from the event after handling dialogue end
        DialogueManager.Instance.OnDialogueEnded -= HandleDialogueEnd;

        // Handle the choice made by the player
        if (selectedChoice == 1)
        {
            if (npcData.npcName == "QuestGiver" && dialogueData.dialogueLines[lineIndex] == "Will you accept this quest?")
            {
                QuestData questToStart = GetQuestToStart();
                if (questToStart != null)
                {
                    QuestManager.Instance.StartQuest(questToStart);
                }
                else
                {
                    Debug.LogError("Quest to start is not assigned or found!");
                }
            }
        }
        else if (selectedChoice == 2)
        {
            // Handle the second choice here, if necessary
        }
    }

    private QuestData GetQuestToStart()
    {
        // Assuming you have a 'questsToOffer' list in your NPCData
        if (npcData != null && npcData.questsToOffer != null && npcData.questsToOffer.Count > 0)
        {
            // Find the first available quest (not yet started or completed)
            foreach (var quest in npcData.questsToOffer)
            {
                if (quest != null && quest.status == QuestData.QuestStatus.NotStarted)
                {
                    return quest;
                }
            }

            // If no available quests, you might want to handle this differently (e.g., provide alternative dialogue)
            Debug.LogWarning("No available quests to offer from this NPC!");
        }
        else
        {
            Debug.LogError("No questsToOffer assigned to this NPC or the list is empty!");
        }

        return null;
    }


}


