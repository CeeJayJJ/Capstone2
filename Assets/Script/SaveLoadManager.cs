using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
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
            achievementsFilePath = Application.persistentDataPath + "/achievements.dat"; // Separate file for achievements
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Button-triggered Save method

    // Save player data
    // Save game data including player and current scene
    public void SaveGame(PlayerData playerData, Transform playerTransform)
    {
        var currentScene = SceneManager.GetActiveScene().name;
        var dataToSave = new PlayerDataToSerialize(playerData, playerTransform.position, currentScene);

        try
        {
            using (FileStream file = File.Create(gameDataFilePath))
            {
                new BinaryFormatter().Serialize(file, dataToSave);
            }
            Debug.Log("Game saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);
        }
    }

    // Load game data
    public void LoadGame(PlayerData playerData, Transform playerTransform)
    {
        if (File.Exists(gameDataFilePath))
        {
            try
            {
                using (FileStream file = File.Open(gameDataFilePath, FileMode.Open))
                {
                    var loadedData = (PlayerDataToSerialize)new BinaryFormatter().Deserialize(file);
                    loadedData.ApplyTo(playerData, playerTransform);
                    SceneManager.LoadScene(loadedData.sceneName);
                }
                Debug.Log("Game loaded successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load game: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }

    // Save inventory data using ItemDataSerializable
    public void SaveInventory(List<ItemDataSerializable> inventoryItems)
    {
        try
        {
            using (FileStream file = File.Create(inventoryFilePath))
            {
                new BinaryFormatter().Serialize(file, inventoryItems);
            }
            Debug.Log("Inventory saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save inventory: " + e.Message);
        }
    }

    // Load inventory data and return a list of ItemData
    public List<ItemData> LoadInventory()
    {
        if (File.Exists(inventoryFilePath))
        {
            try
            {
                using (FileStream file = File.Open(inventoryFilePath, FileMode.Open))
                {
                    var loadedItems = (List<ItemDataSerializable>)new BinaryFormatter().Deserialize(file);
                    List<ItemData> inventoryItems = new List<ItemData>();

                    foreach (var serializableItem in loadedItems)
                    {
                        // Create a new instance of ItemData for each item loaded
                        ItemData item = ScriptableObject.CreateInstance<ItemData>();
                        item.InitializeItem(serializableItem.itemName, serializableItem.itemDescription, LoadIcon(serializableItem.iconPath), serializableItem.itemQuantity);
                        inventoryItems.Add(item);
                    }

                    Debug.Log("Inventory loaded successfully.");
                    return inventoryItems;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load inventory: " + e.Message);
            }
        }
        else
        {
            Debug.Log("No inventory save file found, starting with empty inventory.");
        }

        return new List<ItemData>(); // Return an empty list if no save file is found
    }

    // Load icon from Resources
    private Sprite LoadIcon(string iconName)
    {
        return Resources.Load<Sprite>("Icons/" + iconName); // Adjust the path as necessary
    }



    // Save achievements data
    public void SaveAchievements(List<AchievementManager.Achievement> achievements)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(achievementsFilePath))
            {
                bf.Serialize(file, achievements);
            }
            Debug.Log("Achievements saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save achievements: " + e.Message);
        }
    }

    // Load achievements data
    public List<AchievementManager.Achievement> LoadAchievements()
    {
        if (File.Exists(achievementsFilePath))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(achievementsFilePath, FileMode.Open))
                {
                    var loadedAchievements = (List<AchievementManager.Achievement>)bf.Deserialize(file);
                    Debug.Log("Achievements loaded successfully.");
                    return loadedAchievements;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load achievements: " + e.Message);
            }
        }
        else
        {
            Debug.Log("No achievements save file found, initializing with default achievements.");
        }

        return new List<AchievementManager.Achievement>(); // Return an empty list if no save file is found
    }

    // Utility method to check if a specific file exists
    public static bool FileExists(string fileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + fileName + ".dat");
    }
}



