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
        dataToSave.techbar = PlayerMovement.Instance.playerData.techbar;
        dataToSave.socialbar = PlayerMovement.Instance.playerData.socialbar;
        dataToSave.relationship = PlayerMovement.Instance.playerData.relationship;
        dataToSave.coins = PlayerMovement.Instance.playerData.coins;

        // Copy inventory and quest data
        dataToSave.inventoryItems = new List<ItemData>(PlayerMovement.Instance.playerData.inventoryItems);
        dataToSave.activeQuests = new List<QuestData>(PlayerMovement.Instance.playerData.activeQuests);
        dataToSave.completedQuests = new List<QuestData>(PlayerMovement.Instance.playerData.completedQuests);

        // Save using binary or JSON format
        SaveLoadManager.Save(dataToSave, "GameData");
    }

    public void LoadGame()
    {
        if (SaveLoadManager.FileExists("GameData"))
        {
            GameData loadedData = SaveLoadManager.Load<GameData>("GameData");

            // Populate PlayerData from loaded data
            PlayerMovement.Instance.playerData.techbar = loadedData.techbar;
            PlayerMovement.Instance.playerData.socialbar = loadedData.socialbar;
            PlayerMovement.Instance.playerData.relationship = loadedData.relationship;
            PlayerMovement.Instance.playerData.coins = loadedData.coins;

            // Copy inventory and quest data
            PlayerMovement.Instance.playerData.inventoryItems = new List<ItemData>(loadedData.inventoryItems);
            PlayerMovement  .Instance.playerData.activeQuests = new List<QuestData>(loadedData.activeQuests);
            PlayerMovement.Instance.playerData.completedQuests = new List<QuestData>(loadedData.completedQuests);
        }
    }
}