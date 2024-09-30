using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    void Awake()
    {
        // Singleton implementation
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

    public void SaveGame()
    {
        // Gather data to save
        GameData dataToSave = new GameData();
        dataToSave.playerData = PlayerController.Instance.playerData; // Example: Get player data
        // ... (Gather other data - quest progress, etc.)

        // Binary serialization
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");

        bf.Serialize(file, dataToSave);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
        {
            // Binary deserialization
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);

            GameData loadedData = (GameData)bf.Deserialize(file);
            file.Close();

            // Apply loaded data to the game
            PlayerController.Instance.playerData = loadedData.playerData; // Example: Set player data
            // ... (Apply other loaded data)
        }
    }
}

// Example data structure to hold save data
[System.Serializable]
public class GameData
{
    public PlayerData playerData;
    // ... (Add other data fields as needed)
}