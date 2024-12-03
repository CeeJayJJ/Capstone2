using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyParent : MonoBehaviour
{
    private void Awake()
    {
        // Ensure this GameObject is not destroyed when changing scenes
        DontDestroyOnLoad(gameObject);

        // Check if a duplicate exists
        GameObject[] existingObjects = GameObject.FindGameObjectsWithTag("Persistent");
        foreach (GameObject obj in existingObjects)
        {
            if (obj != this.gameObject)
            {
                Destroy(gameObject); // Destroy duplicate GameObject
                return;
            }
        }
    }
}
