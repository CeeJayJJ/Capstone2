using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    // UI elements (assign these in the Inspector)
    public GameObject dialoguePanel;
    public Text npcNameText;
    public Image npcPortraitImage;
    public Text dialogueText;
    public Button choice1Button, choice2Button;

    // Delegate to handle dialogue end event
    public delegate void DialogueEndedEventHandler(DialogueData dialogueData, int selectedChoice, int lineIndex);
    public event DialogueEndedEventHandler OnDialogueEnded;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private int lastSelectedChoice = 0; // Store the last selected choice

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

    // Start the dialogue system with provided dialogue data
    public void StartDialogue(DialogueData dialogueData)
    {
        currentDialogue = dialogueData;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true); // Show dialogue panel
        DisplayNextLine();
    }

    // Display the next line in the dialogue
    private void DisplayNextLine()
    {
        if (currentDialogue == null || currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        // Update UI with NPC data
        npcNameText.text = currentDialogue.npcName;
        npcPortraitImage.sprite = currentDialogue.npcPortrait;
        dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];

        // Handle dialogue choices if available
        if (currentLineIndex < currentDialogue.choices.Count)
        {
            var currentChoice = currentDialogue.choices[currentLineIndex];
            choice1Button.gameObject.SetActive(true);
            choice2Button.gameObject.SetActive(true);

            choice1Button.GetComponentInChildren<Text>().text = currentChoice.choiceText;
            choice2Button.GetComponentInChildren<Text>().text = currentChoice.choiceText2;

            choice1Button.onClick.RemoveAllListeners();
            choice2Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => OnChoiceSelected(1));
            choice2Button.onClick.AddListener(() => OnChoiceSelected(2));
        }
        else
        {
            // Hide choice buttons if no choices exist for this line
            choice1Button.gameObject.SetActive(false);
            choice2Button.gameObject.SetActive(false);
        }
    }

    // Handle the player's choice selection
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

    // End the dialogue and invoke the event
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false); // Hide dialogue panel
        OnDialogueEnded?.Invoke(currentDialogue, lastSelectedChoice, currentLineIndex); // Trigger event

        currentDialogue = null;
        currentLineIndex = 0; // Reset line index for future dialogues
    }
}

