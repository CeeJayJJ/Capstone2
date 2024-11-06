using UnityEngine;

// Item data structure (name, description, icon, etc.)
[CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemQuantity;
    public int itemID; // Unique identifier for each item

    // Method to initialize item data
    public void InitializeItem(string name, string description, Sprite icon, int quantity, int id)
    {
        itemName = name;
        itemDescription = description;
        itemIcon = icon;
        itemQuantity = quantity;
        itemID = id;
    }

    // Add other item-related properties or methods as needed
}


