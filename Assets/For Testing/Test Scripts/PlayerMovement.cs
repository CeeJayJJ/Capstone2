using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    public Animator animator;

    private NPCInteraction currentNPC;  // To store the current NPC the player can interact with
    public KeyCode interactionKey = KeyCode.E; // The key for interaction, like 'E'
    private NPCData currentNPC1;
    public PlayerData playerData; // Reference to PlayerData ScriptableObject
    public float techbar = 50f, maxTechbar = 100f;

    public static PlayerMovement instance;
    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        rb.gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody component is missing on this GameObject!");
        }

        if (playerData == null)
        {
            Debug.LogError("PlayerData ScriptableObject is not assigned!");
            return;
        }

        // Subscribe to the event for techbar changes in PlayerData
        playerData.OnTechbarChanged += UpdateTechbarUI;

        // Initialize techbar with PlayerData's current value and update the UI
        this.techbar = playerData.Techbar;
        UIManager.Instance.techBar.value = techbar;

        DontDestroyOnLoad(gameObject);
    }

    private void UpdateTechbarUI(float newTechbarValue)
    {
        this.techbar = newTechbarValue; // Update the instance variable
        UIManager.Instance.techBar.value = this.techbar; // Update the UI
    }

    public void ChangeTechbar(float amount)
    {
        // Update the instance variable
        this.techbar += amount;

        // Update PlayerData's techbar to trigger the event
        playerData.Techbar = this.techbar;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        if (playerData != null)
        {
            playerData.OnTechbarChanged -= UpdateTechbarUI;
        }
    }
    private void Update()
    {
 
       
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
                AudioManager.Instance.PlaySFX("Walk");
            }
        }

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 movDir = new Vector3(x, 0, y);

        animator.SetFloat("xMove", movDir.x);
        animator.SetFloat("yMove", movDir.z);
        animator.SetFloat("Speed", movDir.sqrMagnitude);

        rb.velocity = movDir * speed;

        // Check if player presses the interaction key and is near an NPC
        if (currentNPC != null && Input.GetKeyDown(interactionKey))
        {
            UIManager.Instance.interactionPrompt.SetActive(false);
            UIManager.Instance.dialoguePanel.SetActive(true);
            currentNPC.Interact();
        }
    }

    // Detect when the player enters an NPC's interaction range (trigger collider)
    private void OnTriggerEnter(Collider other)
    {
        NPCInteraction npc = other.GetComponent<NPCInteraction>();
        DisplayPlayerDialogue playerDialogue = other.GetComponent<DisplayPlayerDialogue>();
        PerformTeleport performTeleport = other.GetComponent<PerformTeleport>();
        // If player collides with an NPC, handle NPC interaction
        if (npc != null)
        {
            currentNPC = npc;
            UIManager.Instance.interactionPrompt.SetActive(true);  // Show interaction UI
            Debug.Log("Player entered NPC interaction range.");
        }

        // If player collides with a playerDialogue object, start the player dialogue
        else if (playerDialogue != null)
        {
            UIManager.Instance.playerDialoguePanel.SetActive(true);  // Show dialogue panel
            playerDialogue.StartDialogue();  // Start the dialogue
            animator.SetFloat("xMove", 0);
            animator.SetFloat("yMove", 0);
            animator.SetFloat("Speed", 0);
            playerDialogue.StopPlayerMovement();
            
            // Check for dialogue completion (or add event listener when dialogue ends)
            StartCoroutine(WaitForDialogueToEnd(playerDialogue));
        }
    }

    // Coroutine to wait for the dialogue to finish
    private IEnumerator WaitForDialogueToEnd(DisplayPlayerDialogue playerDialogue)
    {
        // While the dialogue panel is still active, wait
        while (UIManager.Instance.playerDialoguePanel.activeSelf)
        {
            yield return null;  // Wait until the next frame, keep looping
        }
    }


    // Detect when the player leaves an NPC's interaction range (trigger collider)
    private void OnTriggerExit(Collider other)
    {
        NPCInteraction npc = other.GetComponent<NPCInteraction>();
        if (npc != null && npc == currentNPC)
        {
            currentNPC = null;
            UIManager.Instance.interactionPrompt.SetActive(false);
            Debug.Log("Player left NPC interaction range.");
        }
    }

    private void InteractWithNPC()
    {
        if (currentNPC != null)
        {
            // Start dialogue with the NPC
            DialogueManager.Instance.StartDialogue(currentNPC1.GetDialogueBasedOnRelationship());

            // Subscribe to dialogue end event
            DialogueManager.Instance.OnDialogueEnded += HandleDialogueEnd;
        }
    }

    private void HandleDialogueEnd(DialogueData dialogueData, int selectedChoice, int lineIndex)
    {
        // Unsubscribe after the dialogue ends to avoid multiple event subscriptions
        DialogueManager.Instance.OnDialogueEnded -= HandleDialogueEnd;

        // Handle the choice made by the player
        if (selectedChoice == 1)
        {
            // If the NPC is offering a quest, handle it
            if (currentNPC1.npcName == "Berto" && dialogueData.dialogueLines[lineIndex] == "Hello Kai, could you help me clean the sewage?")
            {
                QuestData questToStart = currentNPC1.GetFirstAvailableQuest(); // Use the NPC's method
                if (questToStart != null)
                {
                    QuestManager.Instance.StartQuest(questToStart); // Assuming QuestManager exists and handles starting quests
                }
                else
                {
                    Debug.LogError("No available quests to start.");
                }
            }
        }
        else if (selectedChoice == 2)
        {
            // Handle alternative choice here, if necessary
        }
    }
}
