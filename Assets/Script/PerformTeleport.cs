using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerformTeleport : MonoBehaviour
{
    public string sceneName;           // Target scene name
    public string spawnPointName = "SpawnPoint"; // Name of the spawn point object in the target scene

    private void Start()
    {
        // Check if the player object (persistentObjects[0]) exists
        if (DontDestroy.persistentObjects[0] == null)
        {
            Debug.LogError("Player object not found in persistentObjects array! Ensure it is set in DontDestroy.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ensure teleportation happens only for the player
        if (other.gameObject == DontDestroy.persistentObjects[0])
        {
            Debug.Log("Player entered the teleport trigger.");
            StartCoroutine(TeleportPlayer());
        }

        QuestsManager.questsManager.AddQuestItem("Leave the house", 1);
    }

    private IEnumerator TeleportPlayer()
    {
        // Load the target scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // After the scene is loaded, add a slight delay to ensure everything is ready before setting position
        yield return new WaitForSeconds(0.1f);

        // Now that the scene is loaded, place the player at the spawn point
        PlacePlayerAtSpawnPoint();
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










