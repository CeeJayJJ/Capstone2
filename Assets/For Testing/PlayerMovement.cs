using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        rb.gameObject.GetComponent<Rigidbody>();
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
            // Trigger NPC interaction when near NPC
            currentNPC.Interact();
        }
    }

    // Detect when the player enters an NPC's interaction range (trigger collider)
    private void OnTriggerEnter(Collider other)
    {
        NPCInteraction npc = other.GetComponent<NPCInteraction>();
        if (npc != null)
        {
            currentNPC = npc;
            Debug.Log("Player entered NPC interaction range.");
        }
    }

    // Detect when the player leaves an NPC's interaction range (trigger collider)
    private void OnTriggerExit(Collider other)
    {
        NPCInteraction npc = other.GetComponent<NPCInteraction>();
        if (npc != null && npc == currentNPC)
        {
            currentNPC = null;
            Debug.Log("Player left NPC interaction range.");
        }
    }
}
