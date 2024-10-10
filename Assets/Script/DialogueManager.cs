using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    // UI elements (assign these in the Inspector)
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public Image npcPortraitImage;
    public TextMeshProUGUI dialogueText;
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
        // Check if dialogue data is null
        if (currentDialogue == null)
        {
            Debug.LogError("DialogueData is null in DisplayNextLine!");
            return;
        }

        // Ensure currentLineIndex is within bounds of the dialogue lines
        if (currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();  // End dialogue if no more lines
            return;
        }

        // Update UI with NPC data
        npcNameText.text = currentDialogue.npcName;
        npcPortraitImage.sprite = currentDialogue.npcPortrait;
        dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];

        // Check if there are choices available for this dialogue line
        if (currentLineIndex < currentDialogue.choices.Count && !string.IsNullOrEmpty(currentDialogue.choices[currentLineIndex].choiceText))
        {
            var currentChoice = currentDialogue.choices[currentLineIndex];

            // Display choice buttons
            choice1Button.gameObject.SetActive(true);
            choice2Button.gameObject.SetActive(true);

            // Set choice button texts
            choice1Button.GetComponentInChildren<TMPro.TMP_Text>().text = currentChoice.choiceText;
            choice2Button.GetComponentInChildren<TMPro.TMP_Text>().text = currentChoice.choiceText2;

            // Remove previous listeners and add new listeners based on player's choice
            choice1Button.onClick.RemoveAllListeners();
            choice2Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => OnChoiceSelected(1));
            choice2Button.onClick.AddListener(() => OnChoiceSelected(2));
        }
        else
        {
            // No choices for this line, just display the next line
            choice1Button.gameObject.SetActive(false);
            choice2Button.gameObject.SetActive(false);

            // Automatically move to the next dialogue line after a delay or player click
            currentLineIndex++;
            if (currentLineIndex < currentDialogue.dialogueLines.Count)
            {
                // Display the next line if available
                Invoke("DisplayNextLine", 3f); // Add a slight delay to avoid instant jumps
            }
            else
            {
                // End the dialogue when there are no more lines
                EndDialogue();
            }
        }
    }


    // Handle the player's choice selection
    public void OnChoiceSelected(int choiceIndex)
    {
        lastSelectedChoice = choiceIndex;

        // Check if the current line has associated choices
        if (currentLineIndex < currentDialogue.choices.Count)
        {
            var currentChoice = currentDialogue.choices[currentLineIndex];

            // Move to the next dialogue line based on the player's choice
            if (choiceIndex == 1)
            {
                currentLineIndex = currentChoice.nextDialogueIndexIfChosen;  // Update index based on choice 1
            }
            else if (choiceIndex == 2)
            {
                currentLineIndex = currentChoice.nextDialogueIndexIfChosen2;  // Update index based on choice 2
            }

            // After updating the line index, display the next line
            DisplayNextLine();
        }
        else
        {
            Debug.LogError("No valid choices found at this index!");
            EndDialogue();  // Safeguard in case no choices exist at this point
        }
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

