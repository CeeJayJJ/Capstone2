using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; } // Singleton

    void Awake()
    {
        // Singleton implementation (similar to PlayerController)
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        // ... (Logic to display dialogue based on dialogueData)
    }
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Dialogue")]
public class DialogueData : ScriptableObject
{
    // ... (Define your dialogue structure - trees, choices, etc.)
}
