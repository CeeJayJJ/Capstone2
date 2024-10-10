using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "RPG/Player Dialogue")]
public class PlayerDialogue : ScriptableObject
{
    [System.Serializable]
    public struct DialogueLine
    {
        public string line;  // The dialogue line text
        public Sprite portrait;  // The portrait associated with this line
    }

    // List of dialogue lines, each with its own portrait
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
