using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public void SaveGame() => SaveTheGame();
    public void LoadGame() => LoadTheGame();
    public void SaveInventory() => SaveTheInventory();
    public void LoadInventory() => LoadTheInventory();
    public void SaveAchievements() => SaveTheAchievements();
    public void LoadAchievements() => LoadTheAchievements();

    private void SaveTheGame()
    {
        GameData gameData = new GameData();
        SaveLoadManager.Instance.SaveGame(gameData);
    }

    private void LoadTheGame()
    {
        GameData gameData = new GameData();
        if (SaveLoadManager.Instance.IsGameDataAvailable())  // Check if save file exists
        {
            SaveLoadManager.Instance.LoadGame(gameData);
        }
        else
        {
            Debug.LogWarning("No saved game data found.");
        }
    }

    private void SaveTheInventory()
    {
        List<ItemDataSerializable> inventoryData = new List<ItemDataSerializable>();
        SaveLoadManager.Instance.SaveInventory(inventoryData);
    }

    private void LoadTheInventory()
    {
        var inventory = SaveLoadManager.Instance.LoadInventory();
        if (inventory != null)
        {
            Debug.Log("Inventory loaded successfully.");
        }
        else
        {
            Debug.LogWarning("No saved inventory data found.");
        }
    }

    private void SaveTheAchievements()
    {
        GameData gameData = new GameData();
        SaveLoadManager.Instance.SaveAchievements(gameData);
    }

    private void LoadTheAchievements()
    {
        GameData gameData = new GameData();
        if (SaveLoadManager.Instance.IsAchievementsDataAvailable())  // Check if achievements file exists
        {
            SaveLoadManager.Instance.LoadAchievements(gameData);
        }
        else
        {
            Debug.LogWarning("No saved achievements data found.");
        }
    }
}
