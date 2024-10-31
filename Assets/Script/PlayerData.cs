using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "New Player Data", menuName = "RPG/Player Data")]
public class PlayerData : ScriptableObject
{
    public float techbar;
    public float socialbar;
    public int relationship;
    public int coins;
    public Vector3 playerPosition;

    // Changed to hold serialized items only for saving
    public List<ItemData> inventoryItems = new List<ItemData>();
    public List<QuestData> activeQuests = new List<QuestData>();
    public List<QuestData> completedQuests = new List<QuestData>();

    public void AddGold(int amount)
    {
        coins += amount;
        Debug.Log("Added Gold: " + amount);
    }

    public void AddItem(ItemData item)
    {
        var existingItem = inventoryItems.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            existingItem.itemQuantity += item.itemQuantity;
        }
        else
        {
            inventoryItems.Add(item);
        }

        Debug.Log("Added item: " + item.itemName + " Quantity: " + item.itemQuantity);
    }
}

[Serializable]
public class PlayerDataToSerialize
{
    public float techbar;
    public float socialbar;
    public int relationship;
    public int coins;

    // Changed to List<ItemDataSerializable> for serialization
    public List<ItemDataSerializable> inventoryItems = new List<ItemDataSerializable>();
    public List<QuestDataSerializable> activeQuests = new List<QuestDataSerializable>();
    public List<QuestDataSerializable> completedQuests = new List<QuestDataSerializable>();

    public SerializableVector3 position;
    public string sceneName;

    public PlayerDataToSerialize(PlayerData playerData, Vector3 playerPosition, string playerScene)
    {
        techbar = playerData.techbar;
        socialbar = playerData.socialbar;
        relationship = playerData.relationship;
        coins = playerData.coins;

        // Convert ItemData to ItemDataSerializable
        foreach (var item in playerData.inventoryItems)
        {
            inventoryItems.Add(new ItemDataSerializable(item));
        }

        // Convert QuestData to QuestDataSerializable
        foreach (var quest in playerData.activeQuests)
        {
            activeQuests.Add(new QuestDataSerializable(quest));
        }

        foreach (var quest in playerData.completedQuests)
        {
            completedQuests.Add(new QuestDataSerializable(quest));
        }

        position = new SerializableVector3(playerPosition);
        sceneName = playerScene;
    }

    public void ApplyTo(PlayerData playerData, Transform playerTransform)
    {
        playerData.techbar = techbar;
        playerData.socialbar = socialbar;
        playerData.relationship = relationship;
        playerData.coins = coins;

        playerData.inventoryItems.Clear();
        foreach (var serializableItem in inventoryItems)
        {
            ItemData item = ScriptableObject.CreateInstance<ItemData>();
            item.InitializeItem(
                serializableItem.itemName,
                serializableItem.itemDescription,
                LoadIcon(serializableItem.iconPath),
                serializableItem.itemQuantity,
                serializableItem.id  // Pass id here
            );
            playerData.inventoryItems.Add(item);
        }
    
        // Same for quests
        playerData.activeQuests.Clear();
        foreach (var questSerializable in activeQuests)
        {
            playerData.activeQuests.Add(questSerializable.ToQuestData());
        }
        playerData.completedQuests.Clear();
        foreach (var questSerializable in completedQuests)
        {
            playerData.completedQuests.Add(questSerializable.ToQuestData());
        }

        playerTransform.position = position.ToVector3();
    }

    private Sprite LoadIcon(string iconName)
    {
        return Resources.Load<Sprite>("Icons/" + iconName);
    }
}


[Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}

