using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ... (Singleton implementation and playerData remain the same)
    public static PlayerController Instance { get; private set; } // Singleton
    public PlayerData playerData;
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.2f; // Adjust as needed
    public Animator animator;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake()
    {
        // Singleton implementation (similar to other core scripts)
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

        // Get movement input (consider using InputManager here)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);
        moveDirection = transform.TransformDirection(moveDirection); // Convert to world space
        moveDirection.Normalize(); // Ensure consistent speed in all directions

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

        // Update animator (add more parameters as needed for other animations)
        animator.SetFloat("Speed", moveDirection.magnitude);
    }
}