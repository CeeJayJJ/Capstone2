using UnityEngine;

// Core player logic, interactions with Unity systems
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; } // Singleton

    public PlayerData playerData; // ScriptableObject for player-specific data

    void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize player data (e.g., load from save or create new)
    }

    // ... (Methods for movement, combat, etc.)
}
