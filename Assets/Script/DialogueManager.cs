using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    // UI elements (assign these in the Inspector)
    public GameObject dialoguePanel;
    public TextMesh npcNameText;
    public Image npcPortraitImage;
    public TextMesh dialogueText;
    public Button choice1Button, choice2Button;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;

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
                // Display choices
                choice1Button.gameObject.SetActive(true);
                choice2Button.gameObject.SetActive(true);
                choice1Button.GetComponentInChildren<Text>().text = currentDialogue.choices[currentLineIndex].choiceText;
                choice2Button.GetComponentInChildren<Text>().text = currentDialogue.choices[currentLineIndex].choiceText2;
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
        currentDialogue = null;
        currentLineIndex = 0;
    }
}

