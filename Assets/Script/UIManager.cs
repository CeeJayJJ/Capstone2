using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; } // Singleton

    // References to UI elements (panels, text, images, etc.)
    public GameObject dialoguePanel;
    public TMPro.TextMeshProUGUI dialogueText;
    public GameObject questLogPanel;
    public GameObject inventoryPanel;
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

    // Methods to show/hide UI elements
    public void ShowDialogue(string text)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = text;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
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
