using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    void LateUpdate()
    {
        // Find the GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Check if the playerObject was found
        if (playerObject != null)
        {
            // Get the player's position
            Vector3 playerPosition = playerObject.transform.position;

            // Create a new position for the camera or object, maintaining a fixed height
            Vector3 newPosition = playerPosition;
            newPosition.y = transform.position.y; // Maintain the current height of this object

            // Update the position of this object
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }
    }
}
