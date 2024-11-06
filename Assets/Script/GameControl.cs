using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class GameData
{
    // General player information
    public PlayerData playerData;

    // Quest and Inventory data
    public List<QuestData> activeQuests = new List<QuestData>();
    public List<QuestData> completedQuests = new List<QuestData>();
    public List<ItemData> inventoryItems = new List<ItemData>();

    // Achievement data
    public List<AchievementManager.Achievement> activeAchievements = new List<AchievementManager.Achievement>();

    // Player's Transform data
    public Vector3 playerPosition;
    public Quaternion playerRotation;

    // Audio settings
    public float audioVolume = 1.0f;

    // UI elements references (can be set at runtime, no need for serialization)
    [NonSerialized] public GameObject dialoguePanel;
    [NonSerialized] public GameObject playerDialoguePanel;
    [NonSerialized] public GameObject questLogPanel;
    [NonSerialized] public GameObject inventoryPanel;
    [NonSerialized] public GameObject optionPanel;
    [NonSerialized] public GameObject audioPanel;
    [NonSerialized] public GameObject interactionPrompt;

    [NonSerialized] public Slider socialBar;
    [NonSerialized] public Slider techBar;

    [NonSerialized] public Button saveButton;
    [NonSerialized] public Button loadButton;

    // Method to save player position and rotation
    public void SavePlayerTransform(Transform playerTransform)
    {
        playerPosition = playerTransform.position;
        playerRotation = playerTransform.rotation;
    }

    // Method to load player position and rotation into a transform
    public void LoadPlayerTransform(Transform playerTransform)
    {
        playerTransform.position = playerPosition;
        playerTransform.rotation = playerRotation;
    }
}

[Serializable]
public class GameDataToSerialize
{
    // Data fields for saving
    public PlayerData playerData;
    public List<QuestData> activeQuests;
    public List<ItemData> inventoryItems;
    public List<AchievementManager.Achievement> activeAchievements;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public string sceneName;

    // Constructor to initialize data for saving
    public GameDataToSerialize(GameData gameData)
    {
        playerData = gameData.playerData;
        activeQuests = new List<QuestData>(gameData.activeQuests);
        inventoryItems = new List<ItemData>(gameData.inventoryItems);
        activeAchievements = new List<AchievementManager.Achievement>(gameData.activeAchievements);
        playerPosition = gameData.playerPosition;
        playerRotation = gameData.playerRotation;
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Method to apply saved data back to GameData
    public void ApplyTo(GameData gameData)
    {
        gameData.playerData = playerData;
        gameData.activeQuests = new List<QuestData>(activeQuests);
        gameData.inventoryItems = new List<ItemData>(inventoryItems);
        gameData.activeAchievements = new List<AchievementManager.Achievement>(activeAchievements);
        gameData.playerPosition = playerPosition;
        gameData.playerRotation = playerRotation;
    }
}


