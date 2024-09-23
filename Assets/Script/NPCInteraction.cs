using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public NPCData npcData;
    public float interactionRadius = 2f;

    public GameObject interactionPrompt; // UI element for the prompt
    private Canvas interactionPromptCanvas; // To control UI positioning

    private void Start()
    {
        // Get the Canvas component of the interaction prompt (assuming it's a child)
        interactionPromptCanvas = interactionPrompt.GetComponentInChildren<Canvas>();
        if (interactionPromptCanvas == null)
        {
            Debug.LogError("Interaction prompt needs a Canvas component or a child with one!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming your player has the "Player" tag
        {
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CanInteract())
        {
            // Position the interaction prompt above the NPC's head
            PositionInteractionPrompt();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void PositionInteractionPrompt()
    {
        if (interactionPromptCanvas != null)
        {
            // Calculate position above NPC's head (adjust offset as needed)
            Vector3 promptPosition = transform.position + Vector3.up * 2f;

            // Convert world position to screen position
            Vector3 screenPos = Camera.main.WorldToScreenPoint(promptPosition);

            // Set the UI position on the canvas
            RectTransform promptRect = interactionPromptCanvas.GetComponent<RectTransform>();
            promptRect.position = screenPos;
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

