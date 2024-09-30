using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item data structure (name, description, icon, etc.)
[CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item")]
public class ItemData : ScriptableObject
{
    public string itemName; // Add this field to avoid the error
    public string itemDescription;
    public Sprite itemIcon;
    public int itemQuantity;

    // Constructor to initialize item data
    public ItemData(string name, string description, Sprite icon, int quantity)
    {
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
        itemQuantity = quantity;
    }

    // Add other item-related properties or methods as needed
}
