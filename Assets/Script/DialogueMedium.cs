using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // Required for UI elements
public class DialogueMedium : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI npcNameText;
    public Image npcPortraitImage;
    public TextMeshProUGUI dialogueText;
    public Button choice1Button, choice2Button;

    void Awake()
    {
        DialogueManager.Instance.dialoguePanel = this.dialoguePanel;
        DialogueManager.Instance.npcNameText = this.npcNameText;
        DialogueManager.Instance.npcPortraitImage  = this.npcPortraitImage;
        DialogueManager.Instance.dialogueText  = this.dialogueText;
        DialogueManager.Instance.choice1Button = this.choice1Button;
        DialogueManager.Instance.choice2Button = this.choice2Button;
    }
}
