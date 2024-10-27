using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{


    public PlayerData playerData; // Reference to player data (to be assigned)
    public Transform playerTransform; // Reference to player's Transform


    public void SaveGame()
    {
        SaveLoadManager.Instance.SaveGame(playerData, playerTransform);
        Debug.Log("Game Saved.");
    }

    public void LoadGame()
    {
        SaveLoadManager.Instance.LoadGame(playerData, playerTransform);
        Debug.Log("Game Loaded.");
    }
}

