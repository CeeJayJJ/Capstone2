using System.Collections;
using UnityEngine;

public class PerformTeleport : MonoBehaviour
{
    public string sceneName;  // Target scene name

    private void OnTriggerEnter(Collider other)
    {
        // Ensure teleportation happens only for the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the teleport trigger.");

            if (ScenesManager.Instance != null)
            {
                if (UIManager.Instance != null)
                {
                    // Update UIManager references before loading the new scene
                    UIManager.Instance.InitializeUIReferences();
                    Debug.Log("UIManager references initialized.");

                    // Trigger the scene change
                    Debug.Log("ScenesManager found, loading scene: " + sceneName);
                    ScenesManager.Instance.LoadScene(sceneName);
                }
                else
                {
                    Debug.LogError("UIManager.Instance is null, UI references cannot be initialized.");
                }
            }
            else
            {
                Debug.LogError("ScenesManager.Instance is null, cannot load scene.");
            }
        }
    }
}



