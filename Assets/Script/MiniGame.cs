using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public MiniGameConfig config; // ScriptableObject for mini-game configuration

    void Start()
    {
        // Initialize mini-game based on config
    }

    // ... (Methods for mini-game logic)
}

[CreateAssetMenu(fileName = "New Mini-Game Config", menuName = "RPG/Mini-Game Config")]
public class MiniGameConfig : ScriptableObject
{
    // ... (Define your mini-game configuration - rules, scoring, etc.)
}
