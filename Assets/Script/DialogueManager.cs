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

        // Set NPC name and portrait
        npcNameText.text = currentDialogue.npcName;
        npcPortraitImage.sprite = currentDialogue.npcPortrait;
        dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];

        // Check for valid choices
        if (currentLineIndex < currentDialogue.choices.Count && currentDialogue.HasValidChoices(currentLineIndex))
        {
            // Display choice buttons and set their text
            var currentChoice = currentDialogue.choices[currentLineIndex];
            choice1Button.gameObject.SetActive(true);
            choice2Button.gameObject.SetActive(true);

            choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = currentChoice.choiceText;
            choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = currentChoice.choiceText2;

            // Set up the listeners for the choices
            choice1Button.onClick.RemoveAllListeners();
            choice2Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => OnChoiceSelected(1));
            choice2Button.onClick.AddListener(() => OnChoiceSelected(2));
        }
        else
        {
            // If there are no choices, hide the buttons and move to the next line
            choice1Button.gameObject.SetActive(false);
            choice2Button.gameObject.SetActive(false);

            // Increment by 2 to skip the next line
            currentLineIndex+=2;

            if (currentLineIndex <= currentDialogue.dialogueLines.Count)
            {
                Invoke("DisplayNextLine", 3f);  // Add a slight delay before displaying the next line
            }
            else
            {
                EndDialogue();  // End the dialogue if no more lines
            }
        }
    }




    // Handle the player's choice selection
    public void OnChoiceSelected(int choiceIndex)
    {
        if (currentLineIndex < currentDialogue.choices.Count)
        {
            var currentChoice = currentDialogue.choices[currentLineIndex];

            if (choiceIndex == 1)
            {
                currentLineIndex = currentChoice.nextDialogueIndexIfChosen;  // Go to choice 1's dialogue
            }
            else if (choiceIndex == 2)
            {
                currentLineIndex = currentChoice.nextDialogueIndexIfChosen2;  // Go to choice 2's dialogue
            }

            DisplayNextLine();
        }
        else
        {
            Debug.LogError("No valid choices found at this index!");
            EndDialogue();
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

