using TMPro;  // For TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;  // The panel that contains the dialogue UI
    public TextMeshProUGUI dialogueText;  // The text object that will display the dialogue line
    public Image portraitImage;  // The portrait image associated with the dialogue
    public Button nextButton;  // Button to go to the next dialogue line

    public PlayerDialogue playerDialogue;  // Holds all the dialogue data
    private int dialogueIndex = 0;  // Tracks the current line of dialogue
    private PlayerMovement playerMovement;

    void Start()
    {
        // Hide the dialogue panel initially
        dialoguePanel.SetActive(false);

        // Find the player and its movement component (if exists)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }

        // Add a listener to the button to display the next line of dialogue
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    // Function to start the dialogue by showing the first line
    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);  // Show the dialogue panel
        dialogueIndex = 0;  // Start from the first dialogue line
        DisplayNextLine();  // Display the first line
    }

    // Function to display the next line of dialogue
    public void DisplayNextLine()
    {
        // Check if there are still lines left in the dialogue
        if (dialogueIndex < playerDialogue.dialogueLines.Count)
        {
            // Update the dialogue text and portrait
            dialogueText.text = playerDialogue.dialogueLines[dialogueIndex].line;  // Update text with the current line
            portraitImage.sprite = playerDialogue.dialogueLines[dialogueIndex].portrait;  // Update portrait with the current image

            // Move to the next line for the next button press
            dialogueIndex++;
        }
        else
        {
            // If no more lines, end the dialogue
            EndDialogue();
        }
    }

    // Function to hide the dialogue when it's done
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);  // Hide the dialogue panel
        dialogueIndex = 0;  // Reset the dialogue index for next time

        // Re-enable player movement if needed
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
            Destroy(gameObject);
        }
    }

    // Call this method to stop player movement
    public void StopPlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;  // Disable player movement
        }
    }
}
