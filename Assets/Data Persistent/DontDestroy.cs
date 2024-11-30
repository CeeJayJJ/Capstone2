using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static GameObject[] persistentObjects = new GameObject[20];  // Static array to store persistent objects
    public int objectIndex;  // Index for this object in the persistent array

    private void Awake()
    {
        // Ensure the player (index 0) or other objects are set as persistent
        if (persistentObjects[objectIndex] == null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject); // Destroy duplicate objects
        }
    }
}
