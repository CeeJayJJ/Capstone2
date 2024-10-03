using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

// NPC-specific data (name, dialogue options, relationship status, quests, etc.)
[CreateAssetMenu(fileName = "New NPC Data", menuName = "RPG/NPC Data")]
public class NPCData : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait; // Portrait for the dialogue UI
    public int relationshipStatus; // Example: -100 (hostile) to 100 (friendly)
    public List<DialogueData> dialogues; // List of dialogues for this NPC
    public List<QuestData> questsToOffer = new List<QuestData>();

    public enum DialogueType
    {
        Friendly,
        Hostile,
        Neutral
    }

    public DialogueData GetDialogueBasedOnRelationship()
    {
        DialogueType type;

        if (relationshipStatus > 50)
        {
            type = DialogueType.Friendly;
        }
        else if (relationshipStatus < -50)
        {
            type = DialogueType.Hostile;
        }
        else
        {
            type = DialogueType.Neutral;
        }

        // Find and return the first dialogue that matches the determined type
        return dialogues.FirstOrDefault(dialogue => dialogue.dialogueType == type);
    }


    // Optionally, get the first available quest
    public QuestData GetFirstAvailableQuest()
    {
        if (questsToOffer != null && questsToOffer.Count > 0)
        {
            foreach (var quest in questsToOffer)
            {
                if (quest.status == QuestData.QuestStatus.NotStarted)
                {
                    return quest;
                }
            }
        }
        return null; // No available quests
    }


}