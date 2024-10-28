using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMedium : MonoBehaviour
{
    // References to UI elements
    public GameObject dialoguePanel, playerDialoguePanel;
    public GameObject questLogPanel, inventoryPanel;
    public GameObject optionPanel, audioPanel;
    public Slider socialBar, techBar;
    public GameObject interactionPrompt;

    public Button saveButton;
    public Button loadButton;
    public PlayerData playerData;
    public Transform playerTransform;

    void Awake()
    {
        StartCoroutine(InitializeUIReferences());
    }

    // This coroutine will retry the assignment until UIManager is ready
    private System.Collections.IEnumerator InitializeUIReferences()
    {
        // Wait for UIManager to be instantiated if it is null
        while (UIManager.Instance == null)
        {
            yield return null;
        }

        AssignUIElements();
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
        // Re-assign elements in case they are reset in the new scene
        if (UIManager.Instance != null)
        {
            AssignUIElements();
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
        UIManager.Instance.loadButton = loadButton;
        UIManager.Instance.playerData = playerData;
        UIManager.Instance.playerTransform = playerTransform;

        // Initialize UI default states
        UIManager.Instance.InitializeUIReferences();
    }
}

