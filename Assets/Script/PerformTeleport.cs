using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PerformTeleport : MonoBehaviour
{
    public string sceneName;          // Target scene name

    private void OnTriggerEnter(Collider other)
    {
        // Ensure teleportation happens only for the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the teleport trigger.");

            if (ScenesManager.Instance != null)
            {
                Debug.Log("ScenesManager found, loading scene: " + sceneName);
                // Trigger the scene change and pass the spawn point ID
                ScenesManager.Instance.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("ScenesManager.Instance is null, cannot load scene.");
            }
        }
    }
}


