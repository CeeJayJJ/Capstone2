using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait; // Use Sprite instead of Image for efficiency

    // Define the dialogue type (Friendly, Hostile, Neutral)
    public NPCData.DialogueType dialogueType;

    // List of dialogue lines
    public List<string> dialogueLines = new List<string>();

    // List of choices, each with two options and their corresponding next dialogue indices
    public List<Choice> choices = new List<Choice>();

    [System.Serializable]
    public struct Choice
    {
        public string choiceText;
        public int nextDialogueIndexIfChosen;
        public string choiceText2;
        public int nextDialogueIndexIfChosen2;
    }
}