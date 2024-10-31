using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar_Movement : MonoBehaviour
{

    public float MoveSpeed = 3f;


    public float sprintSpeed = 10f;       // Sprinting speed
    public float sprintDuration = 2f;      // Duration for how long the player can sprint
    public float sprintCooldown = 2f;      // Cooldown duration before the player can sprint again
    private bool canSprint = true;         // Variable to track if the player can sprint
    private float sprintTimer = 0f;        // Timer to manage sprint duration and cooldown


    public Rigidbody rigidBody;
    public Animator animator;
    public SpriteRenderer sr;

    Vector3 movement;

    // CROUCH

    private bool isCrouching;

    void Update()
    {
        CharMovement();




    }

    private void FixedUpdate()
    {
        if (!isCrouching)
        {
            rigidBody.MovePosition(rigidBody.position + movement * MoveSpeed * Time.fixedDeltaTime);
        }
    }
    void CharMovement()
    {
        // Check for crouch input
        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true); // Set the crouch animation
            isCrouching = true; // Update crouch state
        }
        else
        {
            animator.SetBool("isCrouching", false); // Reset the crouch animation
            isCrouching = false; // Update crouch state
        }

        // If not crouching, capture movement input
        if (!isCrouching)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");

            HandleSprinting();

            // Set animator parameters for movement
            animator.SetFloat("xHorizontal", movement.x);
            animator.SetFloat("xVertical", movement.z);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            // Reset movement when crouching
            movement = Vector3.zero; // Stop all movement
            animator.SetFloat("Speed", 0); // Set speed to 0 when crouching
        }

    }

    private void HandleSprinting()
    {
        // Check for sprint input
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            // Start sprinting
            MoveSpeed = sprintSpeed; // Increase movement speed
            sprintTimer += Time.deltaTime; // Increment sprint timer

            // If sprint duration is reached, disable sprinting
            if (sprintTimer >= sprintDuration)
            {
                canSprint = false; // Set sprinting to false when duration is reached
            }
        }
        else
        {
            // Reset movement speed if not sprinting
            MoveSpeed = 3f; // Normal speed
        }

        // If sprint timer has elapsed, start cooldown
        if (!canSprint)
        {
            sprintTimer += Time.deltaTime; // Increment the cooldown timer
            if (sprintTimer >= sprintCooldown)
            {
                canSprint = true; // Reset sprinting ability
                sprintTimer = 0f; // Reset timer
            }
        }
    }
    }
