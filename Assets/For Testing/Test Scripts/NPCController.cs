using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Animator animator;
    public string npcName;
    public bool isTalking = false;
    public bool isWalking = false;

    void Update()
    {
        // Switch between states based on NPC behavior
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isTalking", isTalking);

        if (isTalking)
        {
            // Start a conversation or interaction with Kai
            // Trigger quest-related events
        }
    }

    public void StartWalking()
    {
        isWalking = true;
    }

    public void StopWalking()
    {
        isWalking = false;
    }

    public void StartTalking()
    {
        isTalking = true;
    }

    public void StopTalking()
    {
        isTalking = false;
    }
}
