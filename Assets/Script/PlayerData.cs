using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player-specific data (stats, inventory, etc.)
[CreateAssetMenu(fileName = "New Player Data", menuName = "RPG/Player Data")]
public class PlayerData : ScriptableObject
{
    public float techbar;
    public float socialbar;
    public int relationship;
    public int coins;

    // Add inventoryItems to hold the player's inventory
    public List<ItemData> inventoryItems = new List<ItemData>();  // Now contains inventory items

    // You can also add quests or other properties you want to save here
    public List<QuestData> activeQuests = new List<QuestData>();
    public List<QuestData> completedQuests = new List<QuestData>();

    private PlayerMovement PlayerMovement;
   

    // Inside PlayerData
    public void AddGold(int amount)
    {
        coins += amount; // Assuming coins is your currency variable
        Debug.Log("Added Gold: " + amount);
    }

    public void AddItem(ItemData item)
    {
        // Check if item already exists in inventory
        var existingItem = inventoryItems.Find(i => i.itemName == item.itemName);

        if (existingItem != null)
        {
            // Increase quantity if item already exists
            existingItem.itemQuantity += item.itemQuantity;
        }
        else
        {
            // Add a new item to inventory
            inventoryItems.Add(item);
        }

        Debug.Log("Added item: " + item.itemName + " Quantity: " + item.itemQuantity);
    }
}