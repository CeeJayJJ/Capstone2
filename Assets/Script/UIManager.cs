using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;
using System.Collections.Generic;

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
    public Button loadButton;
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

    public void DisplayTechBarCurrentState()
    {
        techBar.value = playerData.techbar;
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

        dialoguePanel = dialoguePanel ?? GameObject.Find("Dialogue Panel");
        playerDialoguePanel = playerDialoguePanel ?? GameObject.Find("Player Dialogue Panel");
        questLogPanel = questLogPanel ?? GameObject.Find("QuestPanel");
        inventoryPanel = inventoryPanel ?? GameObject.Find("InventoryOutlineImg");
        optionPanel = optionPanel ?? GameObject.Find("OptionPanel");
        audioPanel = audioPanel ?? GameObject.Find("Audio Panel");
        interactionPrompt = interactionPrompt ?? GameObject.Find("Interact Panel");

        socialBar = socialBar ?? GameObject.Find("Health Slider")?.GetComponent<Slider>();
        techBar = techBar ?? GameObject.Find("TechSlider")?.GetComponent<Slider>();

        LogMissingReferences();

        // Set default visibility
        interactionPrompt?.SetActive(false);
        dialoguePanel?.SetActive(false);
        playerDialoguePanel?.SetActive(false);
        optionPanel?.SetActive(false);
    }

    private void LogMissingReferences()
    {
        if (dialoguePanel == null) Debug.LogError("Dialogue Panel is missing.");
        if (playerDialoguePanel == null) Debug.LogError("Player Dialogue Panel is missing.");
        if (questLogPanel == null) Debug.LogError("Quest Log Panel is missing.");
        if (inventoryPanel == null) Debug.LogError("Inventory Panel is missing.");
        if (optionPanel == null) Debug.LogError("Option Panel is missing.");
        if (audioPanel == null) Debug.LogError("Audio Panel is missing.");
        if (interactionPrompt == null) Debug.LogError("Interaction Prompt is missing.");
        if (socialBar == null) Debug.LogError("Social Bar is missing.");
        if (techBar == null) Debug.LogError("Tech Bar is missing.");
    }

    // Show methods
    public void ShowDialoguePanel() => dialoguePanel?.SetActive(true);
    public void ShowPlayerDialoguePanel() => playerDialoguePanel?.SetActive(true);
    public void ShowQuestLogPanel() => questLogPanel?.SetActive(true);
    public void ShowInventoryPanel() => inventoryPanel?.SetActive(true);
    public void ShowOptionPanel() => optionPanel?.SetActive(true);
    public void ShowAudioPanel() => audioPanel?.SetActive(true);
    public void ShowInteractionPrompt() => interactionPrompt?.SetActive(true);

    // Hide methods
    public void HideDialoguePanel() => dialoguePanel?.SetActive(false);
    public void HidePlayerDialoguePanel() => playerDialoguePanel?.SetActive(false);
    public void HideQuestLogPanel() => questLogPanel?.SetActive(false);
    public void HideInventoryPanel() => inventoryPanel?.SetActive(false);
    public void HideOptionPanel() => optionPanel?.SetActive(false);
    public void HideAudioPanel() => audioPanel?.SetActive(false);
    public void HideInteractionPrompt() => interactionPrompt?.SetActive(false);

    public void GoToMainMenu() => ScenesManager.Instance.LoadScene("MainMenu");
    public void QuitGame() => Quit();


    private void Quit() 
    { 
        Application.Quit();
    }


}
