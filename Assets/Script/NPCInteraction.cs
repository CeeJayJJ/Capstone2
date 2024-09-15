using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public NPCData npcData; // ScriptableObject for NPC-specific data

    // Optional: For proximity-based interaction triggers
    public float interactionRadius = 2f;

    private void OnMouseDown() // Or OnTriggerEnter for proximity-based interaction
    {
        if (CanInteract())
        {
            Interact();
        }
    }

    private bool CanInteract()
    {
        // Check if the player is within interaction range (if applicable)
        if (interactionRadius > 0f)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
            if (distanceToPlayer > interactionRadius)
            {
                return false;
            }
        }

        // Add any other interaction conditions here (e.g., quest requirements)
        return true;
    }

    private void Interact()
    {
        // Update relationship status
        npcData.relationshipStatus += CalculateRelationshipChange();

        // Trigger dialogue or other interactions
        DialogueManager.Instance.StartDialogue(npcData.GetDialogueBasedOnRelationship());

        // Notify QuestManager if interaction affects any quests
        // ...

        // Potentially trigger mini-games or other gameplay elements
        // ...
    }

    private int CalculateRelationshipChange()
    {
        // ... (Logic to calculate relationship change based on player actions, dialogue choices, etc.)
        return 0; // Placeholder
    }
}

