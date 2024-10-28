using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // UI elements
    public GameObject dialoguePanel, playerDialoguePanel;
    public GameObject questLogPanel, inventoryPanel;
    public GameObject optionPanel, audioPanel;
    public Slider socialBar, techBar;
    public GameObject interactionPrompt;
    public Button saveButton;
    public PlayerData playerData;
    public Transform playerTransform;

    private void Awake()
    {
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeUIReferences();
    }

    public void InitializeUIReferences()
    {
        Debug.Log("Initializing UI references...");

        if (dialoguePanel == null) dialoguePanel = GameObject.Find("Dialogue Panel");
        if (playerDialoguePanel == null) playerDialoguePanel = GameObject.Find("Player Dialogue Panel");
        if (questLogPanel == null) questLogPanel = GameObject.Find("QuestPanel");
        if (inventoryPanel == null) inventoryPanel = GameObject.Find("InventoryOutlineImg");
        if (optionPanel == null) optionPanel = GameObject.Find("OptionPanel");
        if (audioPanel == null) audioPanel = GameObject.Find("Audio Panel");
        if (interactionPrompt == null) interactionPrompt = GameObject.Find("Interact Panel");

        if (socialBar == null) socialBar = GameObject.Find("Health Slider")?.GetComponent<Slider>();
        if (techBar == null) techBar = GameObject.Find("TechSlider")?.GetComponent<Slider>();

        if (optionPanel == null)
        {
            Debug.LogError("OptionPanel is still missing after initialization attempts.");
        }
        else
        {
            Debug.Log("OptionPanel successfully assigned.");
        }

        interactionPrompt?.SetActive(false);
        dialoguePanel?.SetActive(false);
        playerDialoguePanel?.SetActive(false);
        optionPanel?.SetActive(false);
    }


    public void ShowOption()
    {
        if (optionPanel == null)
        {
            InitializeUIReferences();
        }

        if (optionPanel != null)
        {
            optionPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Option panel is still missing even after trying to initialize references.");
        }
    }

    public void HideOption()
    {
        optionPanel?.SetActive(false);
    }
    private void SaveGame()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData is null!");
            return;
        }
        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransform is null!");
            return;
        }
        if (SaveLoadManager.Instance == null)
        {
            Debug.LogError("SaveLoadManager.Instance is null!");
            return;
        }

        // Call save method
        SaveLoadManager.Instance.SaveGame(playerData, playerTransform);
    }

    private void LoadGame()
    {
        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.LoadGame(playerData, playerTransform);
        }
    }

    // Methods to show/hide UI elements
    public void ShowPlayerDialogue() => playerDialoguePanel?.SetActive(true);
    public void ShowDialogue(string text) => dialoguePanel?.SetActive(true);
    public void HideDialogue() => dialoguePanel?.SetActive(false);
    public void ShowAudio() => audioPanel?.SetActive(true);
    public void HideAudio() => audioPanel?.SetActive(false);
    public void ShowQuestLog() => questLogPanel?.SetActive(true);
    public void HideQuestLog() => questLogPanel?.SetActive(false);
    public void ShowInventory() => inventoryPanel?.SetActive(true);
    public void HideInventory() => inventoryPanel?.SetActive(false);

    private void Update()
    {
        if (interactionPrompt == null)
        {
            interactionPrompt = GameObject.Find("InteractionPrompt");
        }
    }
}

