using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;  // Drag the player here in the Inspector

    void LateUpdate()
    {
        // Follow the player's position, but keep the camera's height constant
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;  // Keep camera at a fixed height
        transform.position = newPosition;
    }
}
