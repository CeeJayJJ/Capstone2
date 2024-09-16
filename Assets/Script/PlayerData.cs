using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player-specific data (stats, inventory, etc.)
[CreateAssetMenu(fileName = "New Player Data", menuName = "RPG/Player Data")]
public class PlayerData : ScriptableObject
{
    public float techbar;
    public float socialbar;
    public int relationship;
    public int coins;
}
