using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [System.Serializable] // Ensure the struct is serializable
    public struct Choice
    {
        public string choiceText;         // Text for the first choice
        public int nextDialogueIndexIfChosen; // Index to jump to if choice 1 is selected
        public string choiceText2;        // Text for the second choice
        public int nextDialogueIndexIfChosen2; // Index to jump to if choice 2 is selected
    }

    // Method to check if choices are valid
    public bool HasValidChoices(int index)
    {
        if (index < choices.Count)
        {
            Choice choice = choices[index];
            // Ensure both choices are properly set
            return !string.IsNullOrEmpty(choice.choiceText) && !string.IsNullOrEmpty(choice.choiceText2);
        }
        return false;
    }

    // Debug method to print out dialogue data
    public void DebugDialogueData()
    {
        Debug.Log("NPC Name: " + npcName);
        Debug.Log("Dialogue Lines: " + dialogueLines.Count);
        Debug.Log("Choices Count: " + choices.Count);
    }
}
