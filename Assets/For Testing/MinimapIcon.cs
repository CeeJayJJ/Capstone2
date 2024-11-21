using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
   
    void LateUpdate()
    {
        // Find the GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindWithTag("Player");

        // Check if the playerObject was found
        if (playerObject != null)
        {
            // Get the player's rotation
            Vector3 playerRotation = playerObject.transform.eulerAngles;

            // Set this object's rotation to match the player's y rotation
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, playerRotation.y, transform.eulerAngles.z);
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }
    }
}
