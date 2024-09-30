using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player-specific data (stats, inventory, etc.)
[CreateAssetMenu(fileName = "New Player Data", menuName = "RPG/Player Data")]
public class PlayerData : ScriptableObject
{
    public float techbar;
    public float socialbar;
    public int relationship;
    public int coins;

    // Add inventoryItems to hold the player's inventory
    public List<ItemData> inventoryItems = new List<ItemData>();  // Now contains inventory items

    // You can also add quests or other properties you want to save here
    public List<QuestData> activeQuests = new List<QuestData>();
    public List<QuestData> completedQuests = new List<QuestData>();


    public void SaveGame()
    {
        GameData dataToSave = new GameData();

        // Gather data from PlayerData ScriptableObject
        dataToSave.techbar = PlayerController.Instance.playerData.techbar;
        dataToSave.socialbar = PlayerController.Instance.playerData.socialbar;
        dataToSave.relationship = PlayerController.Instance.playerData.relationship;
        dataToSave.coins = PlayerController.Instance.playerData.coins;

        // Copy inventory and quest data
        dataToSave.inventoryItems = new List<ItemData>(PlayerController.Instance.playerData.inventoryItems);
        dataToSave.activeQuests = new List<QuestData>(PlayerController.Instance.playerData.activeQuests);
        dataToSave.completedQuests = new List<QuestData>(PlayerController.Instance.playerData.completedQuests);

        // Save using binary or JSON format
        SaveLoadManager.Save(dataToSave, "GameData");
    }

    public void LoadGame()
    {
        if (SaveLoadManager.FileExists("GameData"))
        {
            GameData loadedData = SaveLoadManager.Load<GameData>("GameData");

            // Populate PlayerData from loaded data
            PlayerController.Instance.playerData.techbar = loadedData.techbar;
            PlayerController.Instance.playerData.socialbar = loadedData.socialbar;
            PlayerController.Instance.playerData.relationship = loadedData.relationship;
            PlayerController.Instance.playerData.coins = loadedData.coins;

            // Copy inventory and quest data
            PlayerController.Instance.playerData.inventoryItems = new List<ItemData>(loadedData.inventoryItems);
            PlayerController.Instance.playerData.activeQuests = new List<QuestData>(loadedData.activeQuests);
            PlayerController.Instance.playerData.completedQuests = new List<QuestData>(loadedData.completedQuests);
        }
    }
}
