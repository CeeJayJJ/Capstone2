using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    private string gameDataFilePath;
    private string inventoryFilePath;
    private string achievementsFilePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameDataFilePath = Application.persistentDataPath + "/gamedata.dat";
            inventoryFilePath = Application.persistentDataPath + "/inventory.dat";
            achievementsFilePath = Application.persistentDataPath + "/achievements.dat";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsGameDataAvailable() => File.Exists(gameDataFilePath);
    public bool IsInventoryDataAvailable() => File.Exists(inventoryFilePath);
    public bool IsAchievementsDataAvailable() => File.Exists(achievementsFilePath);

    // Main Save method triggered by a button or event
    public void SaveGame(GameData gameData)
    {
        var dataToSave = new GameDataToSerialize(gameData);

        try
        {
            using (FileStream file = File.Create(gameDataFilePath))
            {
                new BinaryFormatter().Serialize(file, dataToSave);
            }
            Debug.Log("Game saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);
        }
    }

    // Load method for loading the main game data
    public void LoadGame(GameData gameData)
    {
        if (File.Exists(gameDataFilePath))
        {
            try
            {
                using (FileStream file = File.Open(gameDataFilePath, FileMode.Open))
                {
                    var loadedData = (GameDataToSerialize)new BinaryFormatter().Deserialize(file);
                    loadedData.ApplyTo(gameData);
                    SceneManager.LoadScene(loadedData.sceneName);
                }
                Debug.Log("Game loaded successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load game: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }

    // Save inventory data
    public void SaveInventory(List<ItemDataSerializable> inventory)
    {
        try
        {
            using (FileStream file = File.Create(inventoryFilePath))
            {
                new BinaryFormatter().Serialize(file, inventory);
            }
            Debug.Log("Inventory saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save inventory: " + e.Message);
        }
    }

    // Load inventory data
    public List<ItemData> LoadInventory()
    {
        if (File.Exists(inventoryFilePath))
        {
            try
            {
                using (FileStream file = File.Open(inventoryFilePath, FileMode.Open))
                {
                    var loadedInventory = (List<ItemDataSerializable>)new BinaryFormatter().Deserialize(file);
                    List<ItemData> itemList = new List<ItemData>();

                    // Convert Serializable items back to ItemData
                    foreach (var itemSerializable in loadedInventory)
                    {
                        itemList.Add(itemSerializable.ToItemData());
                    }

                    Debug.Log("Inventory loaded successfully.");
                    return itemList;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load inventory: " + e.Message);
                return new List<ItemData>();
            }
        }
        else
        {
            Debug.LogWarning("Inventory file not found.");
            return new List<ItemData>();
        }
    }

    // Save Achievements Data to a separate file
    public void SaveAchievements(GameData gameData)
    {
        try
        {
            using (FileStream file = File.Create(achievementsFilePath))
            {
                new BinaryFormatter().Serialize(file, gameData.activeQuests);
            }
            Debug.Log("Achievements saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save achievements: " + e.Message);
        }
    }

    // Load Achievements Data
    public void LoadAchievements(GameData gameData)
    {
        if (File.Exists(achievementsFilePath))
        {
            try
            {
                using (FileStream file = File.Open(achievementsFilePath, FileMode.Open))
                {
                    gameData.activeQuests = (List<QuestData>)new BinaryFormatter().Deserialize(file);
                }
                Debug.Log("Achievements loaded successfully.");
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to load achievements: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Achievements file not found.");
        }
    }
}



