using UnityEngine;
using UnityEngine.SceneManagement;

public class PerformTeleport : MonoBehaviour
{
    public string sceneName;                   // Target scene name
    public string spawnPointName = "SpawnPoint"; // Name of the spawn point object in the target scene

    private void Start()
    {
        // Check if the player object (persistentObjects[0]) exists
        if (DontDestroy.persistentObjects[0] == null)
        {
            Debug.LogError("Player object not found in persistentObjects array! Ensure it is set in DontDestroy.");
        }

        // Register the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unregister the scene loaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure teleportation happens only for the player
        if (other.gameObject == DontDestroy.persistentObjects[0])
        {
            Debug.Log("Player entered the teleport trigger.");
            SceneManager.LoadScene(sceneName);
            QuestsManager.questsManager.AddQuestItem("Leave the house", 1);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only place the player if the correct scene is loaded
        if (scene.name == sceneName)
        {
            PlacePlayerAtSpawnPoint();
        }
    }

    private void PlacePlayerAtSpawnPoint()
    {
        // Get the player from the persistentObjects array
        GameObject playerObject = DontDestroy.persistentObjects[0];

        if (playerObject != null)
        {
            // Locate the spawn point by name in the new scene
            GameObject spawnPoint = GameObject.Find(spawnPointName);

            if (spawnPoint != null)
            {
                // Set the player's position and rotation to match the spawn point's
                playerObject.transform.position = spawnPoint.transform.position;
                playerObject.transform.rotation = spawnPoint.transform.rotation;
                Debug.Log("Player positioned at spawn point: " + spawnPointName);
            }
            else
            {
                Debug.LogError("Spawn point object '" + spawnPointName + "' not found in the scene!");
            }
        }
        else
        {
            Debug.LogError("Player object not found in persistentObjects array!");
        }
    }
}











