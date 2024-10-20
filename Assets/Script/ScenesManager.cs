using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public string spawnPointID;  // Track spawn point ID across scenes
    public static ScenesManager Instance { get; private set; } // Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Method to load a new scene and pass the spawn point ID
    public void LoadScene(string sceneName, string newSpawnPointID)
    {
        spawnPointID = newSpawnPointID;  // Set the target spawn point for the next scene
        SceneManager.LoadScene(sceneName);
    }
}