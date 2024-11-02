using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerformTeleport : MonoBehaviour
{
    public string sceneName;  // Target scene name
    public Vector3 targetPosition; // Target position in the new scene
    public Vector3 targetRotation; // Optional: Target rotation if you want the player facing a specific direction

    private void OnTriggerEnter(Collider other)
    {
        // Ensure teleportation happens only for the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the teleport trigger.");

            // Subscribe to the sceneLoaded event
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Load the target scene
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the target scene
        if (scene.name == sceneName)
        {
            // Find the player object by tag
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                // Set the player's position and rotation
                player.transform.position = targetPosition;
                player.transform.rotation = Quaternion.Euler(targetRotation);
            }

            // Unsubscribe from the event to prevent this method from being called multiple times
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}




