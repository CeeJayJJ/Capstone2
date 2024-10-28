using UnityEngine;
using UnityEngine.UI;

public class UIMedium : MonoBehaviour
{
    // References to UI elements
    public GameObject dialoguePanel, playerDialoguePanel;
    public GameObject questLogPanel, inventoryPanel;
    public GameObject optionPanel, audioPanel;
    public Slider socialBar, techBar;
    public GameObject interactionPrompt;

    public Button saveButton;
    public PlayerData playerData;
    public Transform playerTransform;

    void Awake()
    {
        // Check if the UIManager instance exists to prevent null reference errors
        if (UIManager.Instance != null)
        {
            AssignUIElements();
        }
        else
        {
            Debug.LogWarning("UIManager instance not found. Make sure UIManager is set up correctly.");
        }
    }

    private void AssignUIElements()
    {
        // Assign all UI elements to UIManager's instance
        UIManager.Instance.dialoguePanel = dialoguePanel;
        UIManager.Instance.playerDialoguePanel = playerDialoguePanel;
        UIManager.Instance.questLogPanel = questLogPanel;
        UIManager.Instance.inventoryPanel = inventoryPanel;
        UIManager.Instance.optionPanel = optionPanel;
        UIManager.Instance.audioPanel = audioPanel;
        UIManager.Instance.socialBar = socialBar;
        UIManager.Instance.techBar = techBar;
        UIManager.Instance.interactionPrompt = interactionPrompt;
        UIManager.Instance.saveButton = saveButton;
        UIManager.Instance.playerData = playerData;
        UIManager.Instance.playerTransform = playerTransform;

        // Initialize the UI elements' default states
        UIManager.Instance.InitializeUIReferences();
    }
}

