using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        // Match the player's Y-axis rotation
        Vector3 rotation = transform.eulerAngles;
        rotation.y = player.eulerAngles.y;
        transform.eulerAngles = rotation;
    }
}
