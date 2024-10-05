using UnityEngine;
using UnityEngine.UI;
using static NPCData;

public class NPCInteraction : MonoBehaviour
{
    public NPCData npcData;
    public float interactionRadius = 2f;

    // Reference to the PlayerMovement script
    private PlayerMovement playerMovement;

    private void Start()
    {    
        UIManager.Instance.interactionPrompt.SetActive(false);
        // Assuming PlayerMovement is attached to the Player with the "Player" tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.interactionPrompt.SetActive(true);
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
                Interact();  // Call the public Interact method
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.interactionPrompt.SetActive(false);
        }
    }

    private void PositionInteractionPrompt()
    {
        if (UIManager.Instance.interactionPrompt != null)
        {
            // Calculate position above NPC's head (adjust offset as needed)
            Vector3 promptPosition = transform.position + Vector3.up * 2f;

            // Convert world position to screen position
            Vector3 screenPos = Camera.main.WorldToScreenPoint(promptPosition);
        }
    }

    private bool CanInteract()
    {
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement is not available!");
            return false;
        }

        // Check if the player is within interaction range
        if (interactionRadius > 0f)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerMovement.transform.position);
            if (distanceToPlayer > interactionRadius)
            {
                return false;
            }
        }

        return true;
    }

    // Change this method to public so that other scripts can access it
    public void Interact()
    {
        // Update relationship status
        npcData.relationshipStatus += CalculateRelationshipChange();

        // Trigger dialogue or other interactions
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(npcData.GetDialogueBasedOnRelationship());

            // Listen for dialogue end event
            DialogueManager.Instance.OnDialogueEnded += HandleDialogueEnd;
        }
        else
        {
            Debug.LogError("DialogueManager instance is not found!");
        }
    }

    private void HandleDialogueEnd(DialogueData dialogueData, int selectedChoice, int lineIndex)
    {
        // Unsubscribe from the event after handling dialogue end
        DialogueManager.Instance.OnDialogueEnded -= HandleDialogueEnd;

        if (selectedChoice == 1)
        {
            if (npcData.npcName == "QuestGiver" && dialogueData.dialogueLines[lineIndex] == "Will you accept this quest?")
            {
                QuestData questToStart = GetQuestToStart();
                if (questToStart != null)
                {
                    QuestManager.Instance.StartQuest(questToStart);
                }
                else
                {
                    Debug.LogError("Quest to start is not assigned or found!");
                }
            }
        }
        // Handle other choices here
    }

    private QuestData GetQuestToStart()
    {
        if (npcData != null && npcData.questsToOffer != null && npcData.questsToOffer.Count > 0)
        {
            foreach (var quest in npcData.questsToOffer)
            {
                if (quest != null && quest.status == QuestData.QuestStatus.NotStarted)
                {
                    return quest;
                }
            }

            Debug.LogWarning("No available quests to offer from this NPC!");
        }
        else
        {
            Debug.LogError("No questsToOffer assigned to this NPC or the list is empty!");
        }

        return null;
    }

    

    private int CalculateRelationshipChange()
    {
        // Add your logic for calculating relationship change based on player's actions
        return 0; // Placeholder value
    }
}





