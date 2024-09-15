using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC-specific data (name, dialogue options, relationship status, etc.)
[CreateAssetMenu(fileName = "New NPC Data", menuName = "RPG/NPC Data")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public int relationshipStatus; // Example: -100 (hostile) to 100 (friendly)

    // ... (Other NPC data like portrait, dialogues, etc.)

    public DialogueData GetDialogueBasedOnRelationship()
    {
        // ... (Logic to select appropriate dialogue based on relationshipStatus)
        return null; // Placeholder
    }
}
