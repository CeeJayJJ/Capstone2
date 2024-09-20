using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; } // Singleton

    private void Awake()
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
    }

    // Methods to get player input (e.g., movement, actions)
    public Vector2 GetMovementInput()
    {
        // ... (Logic to get movement input from keyboard/controller/etc.)
        return Vector2.zero; // Placeholder
    }

    public bool IsActionButtonPressed()
    {
        // ... (Logic to check if the action button is pressed)
        return false; // Placeholder
    }

    // ... other input-related methods
}
