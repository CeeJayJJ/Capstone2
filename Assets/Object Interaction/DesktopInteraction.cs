using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DesktopInteraction : MonoBehaviour
{
    [SerializeField]private GameObject DesktopPanel, interactPanel;
    [SerializeField]private float interactionRadius = 2f;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        // Check if player is tagged correctly
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement component is missing on the Player object!");
            }
        }
        else
        {
            Debug.LogError("Player GameObject with the 'Player' tag was not found!");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPanel.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && CanInteract())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Time.timeScale = 0;
                Interact();  // Call the public Interact method
            }
        }
    }

    private void Interact()
    {
        DesktopPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPanel.SetActive(false);
        }
    }
}
