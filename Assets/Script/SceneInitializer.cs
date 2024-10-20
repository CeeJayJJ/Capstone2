using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    private void Start()
    {
        // Find the spawn point in the new scene based on the ID stored in the ScenesManager
        SpawnPoint targetSpawn = FindSpawnPoint(ScenesManager.Instance.spawnPointID);
        if (targetSpawn != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = targetSpawn.transform.position;  // Move player to the correct spawn point
                Debug.Log("Player moved to spawn point: " + targetSpawn.spawnPointID);
            }
        }
        else
        {
            Debug.LogError("No spawn point found with ID: " + ScenesManager.Instance.spawnPointID);
        }
    }

    private SpawnPoint FindSpawnPoint(string spawnPointID)
    {
        // Search through all spawn points in the scene
        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.spawnPointID == spawnPointID)
            {
                return spawnPoint;
            }
        }
        return null;
    }
}


