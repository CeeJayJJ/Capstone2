using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerIndicator : MonoBehaviour
{
    public Transform player;            // Reference to the player transform
    public RectTransform indicator;     // Reference to the minimap indicator
    public float minimapSize = 100f;    // Size of the minimap

    void Update()
    {
        if (player != null && indicator != null)
        {
            // Convert the player's world position to the minimap's local position
            Vector3 playerPos = player.position;

            // Normalize the player's position based on the minimap size
            float normalizedX = playerPos.x / minimapSize * 0.5f; // Adjust for minimap scale
            float normalizedZ = playerPos.z / minimapSize * 0.5f; // Adjust for minimap scale

            // Set the position of the indicator
            indicator.anchoredPosition = new Vector2(normalizedX * minimapSize, normalizedZ * minimapSize);
        }
    }
}
