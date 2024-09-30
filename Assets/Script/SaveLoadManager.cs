using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    private string saveFilePath;

    void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Application.persistentDataPath + "/gamedata.dat"; // File path for saving game data
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SaveGame()
    {
        // Gather data to save
        GameData dataToSave = new GameData();
        dataToSave.playerData = PlayerController.Instance.playerData; // Save player data
        dataToSave.questsData = QuestManager.Instance.GetQuestsData(); // Save quest progress
        dataToSave.achievementsData = AchievementManager.Instance.GetAchievementsData(); // Save achievements

        // Binary serialization
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(saveFilePath))
            {
                bf.Serialize(file, dataToSave);
            }
            Debug.Log("Game saved successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save game: " + e.Message);
        }
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                // Binary deserialization
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(saveFilePath, FileMode.Open))
                {
                    GameData loadedData = (GameData)bf.Deserialize(file);

                    // Apply loaded data to the game
                    PlayerController.Instance.playerData = loadedData.playerData; // Load player data
                    QuestManager.Instance.LoadQuestsData(loadedData.questsData); // Load quest progress
                    AchievementManager.Instance.LoadAchievementsData(loadedData.achievementsData); // Load achievements
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

    // Method to check if a save file exists
    public static bool FileExists(string fileName)
    {
        return File.Exists(Application.persistentDataPath + "/" + fileName + ".dat");
    }

    // Generic method to save data
    public static void Save<T>(T data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Data saved to: " + path);
    }

    // Generic method to load data
    public static T Load<T>(string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".dat";

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            T data = (T)bf.Deserialize(file);
            file.Close();

            Debug.Log("Data loaded from: " + path);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found: " + path);
            return default;
        }
    }
}

// Data structure to hold all saveable game data
[System.Serializable]
public class GameData
{
    public PlayerData playerData;  // Stores player-specific data
    public QuestData[] questsData; // Stores quest progress (array of quest data)
    public Dictionary<string, bool> achievementsData; // Stores achievement progress

    public float techbar;
    public float socialbar;
    public int relationship;
    public int coins;

    public List<ItemData> inventoryItems;   // Save inventory items
    public List<QuestData> activeQuests;    // Save active quests
    public List<QuestData> completedQuests; // Save completed quests
}
