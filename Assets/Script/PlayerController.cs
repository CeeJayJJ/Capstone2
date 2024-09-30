using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public PlayerData playerData;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.2f;
    public Animator animator;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private NPCData currentNPC; // Reference to the NPC being interacted with

    
    void Awake()
    {
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

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("PlayerController requires a CharacterController component!");
        }
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 0.1f, groundCheckDistance, groundLayer);

        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.Normalize();

        // Apply movement
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        // Update animator
        animator.SetFloat("Speed", moveDirection.magnitude);

        // Interact with NPC if within range and pressing the interact button
        if (currentNPC != null && Input.GetButtonDown("Interact"))
        {
            InteractWithNPC();
        }
    }

    // Trigger for NPC interaction (assumes you have a collider on NPCs)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPCInteraction>().npcData; // Get the NPC data
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = null;
        }
    }

    private void InteractWithNPC()
    {
        if (currentNPC != null)
        {
            // Start dialogue with the NPC
            DialogueManager.Instance.StartDialogue(currentNPC.GetDialogueBasedOnRelationship());

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
            if (currentNPC.npcName == "QuestGiver" && dialogueData.dialogueLines[lineIndex] == "Will you accept this quest?")
            {
                QuestData questToStart = currentNPC.GetFirstAvailableQuest(); // Use the NPC's method
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
