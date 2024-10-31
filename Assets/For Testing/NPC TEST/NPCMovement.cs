using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2f;       // Speed of the NPC
    public float moveDistance = 5f;     // Distance to move back and forth

    private Vector3 startingPosition;   // Start position of the NPC
    private Vector3 targetPosition;      // Target position to move towards
    private Animator animator;           // Reference to the animator
    private bool movingToTarget = true; // Flag to indicate direction of movement

    private void Start()
    {
        startingPosition = transform.position; // Store the starting position
        targetPosition = startingPosition + new Vector3(moveDistance, 0, 0); // Calculate target position
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    private void Update()
    {
        // Move the NPC
        MoveNPC();
    }

    private void MoveNPC()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the NPC has reached the target position
        if (transform.position == targetPosition)
        {
            // Switch direction
            movingToTarget = !movingToTarget;

            // Update the target position
            if (movingToTarget)
            {
                targetPosition = startingPosition + new Vector3(moveDistance, 0, 0); // Move to right
                animator.SetBool("isWalkingRight", true);
                animator.SetBool("isWalkingLeft", false);
            }
            else
            {
                targetPosition = startingPosition - new Vector3(moveDistance, 0, 0); // Move to left
                animator.SetBool("isWalkingRight", false);
                animator.SetBool("isWalkingLeft", true);
            }
        }

        // Update animator to play the walking animation while moving
        if (movingToTarget)
        {
            animator.SetBool("isWalkingRight", targetPosition.x > transform.position.x);
            animator.SetBool("isWalkingLeft", targetPosition.x < transform.position.x);
        }
    }
}
