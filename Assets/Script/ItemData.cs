using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item data structure (name, description, icon, etc.)
[CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemQuantity;

    // Method to initialize item data
    public void InitializeItem(string name, string description, Sprite icon, int quantity)
    {
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
        itemQuantity = quantity;
    }

    // Add other item-related properties or methods as needed
}
