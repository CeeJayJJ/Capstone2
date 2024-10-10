using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton

    // References to UI elements (panels, text, images, etc.)
    public GameObject dialoguePanel, playerDialoguePanel;
    public GameObject questLogPanel;
    public GameObject inventoryPanel;
    public GameObject optionPanel, audioPanel;
    public Slider socialBar, techBar;
    public GameObject interactionPrompt;
    // ... other UI elements

    private void Awake()
    {
        // Singleton implementation (similar to other core scripts)
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
    }
    private void Start()
    {
        interactionPrompt.SetActive(false);
        dialoguePanel.SetActive(false);
        //playerDialoguePanel.SetActive(false);
    }

    public void ShowPlayerDialogue()
    {
        playerDialoguePanel.SetActive(true);
    }
    // Methods to show/hide UI elements
    public void ShowDialogue(string text)
    {
        dialoguePanel.SetActive(true);
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ShowOption()
    {
        optionPanel.SetActive(true);
    }

    public void HideOption()
    {
        optionPanel.SetActive(false);
    }

    public void ShowAudio()
    {
        audioPanel.SetActive(true);
    }

    public void HideAudio()
    {
        audioPanel.SetActive(false);
    }

    public void ShowQuestLog()
    {
        questLogPanel.SetActive(true);
        // Populate quest log UI with data from QuestManager
    }

    public void HideQuestLog()
    {
        questLogPanel.SetActive(false);
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        // Populate inventory UI with data from InventoryManager
    }

    public void HideInventory()
    {
        inventoryPanel.SetActive(false);
    }

    // ... other UI management methods
}
