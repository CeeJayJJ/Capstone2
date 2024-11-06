using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private List<ItemData> inventoryItems = new List<ItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadInventory(); // Load inventory on startup
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddItem(ItemData item)
    {
        inventoryItems.Add(item);
        Debug.Log("Item added: " + item.itemName);
    }

    public void RemoveItem(ItemData item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            Debug.Log("Item removed: " + item.itemName);
        }
        else
        {
            Debug.LogWarning("Item not found in inventory: " + item.itemName);
        }
    }

    public bool HasItem(ItemData item)
    {
        return inventoryItems.Contains(item);
    }

    public List<ItemData> GetInventoryItems()
    {
        return new List<ItemData>(inventoryItems); // Return a copy to prevent external modification
    }

    public void SaveInventory()
    {
        List<ItemDataSerializable> serializableInventory = new List<ItemDataSerializable>();
        foreach (var item in inventoryItems) // Use the inventoryItems directly
        {
            serializableInventory.Add(new ItemDataSerializable(item));
        }
        SaveLoadManager.Instance.SaveInventory(serializableInventory);
    }

    public void LoadInventory()
    {
        inventoryItems = SaveLoadManager.Instance.LoadInventory();
        Debug.Log("Inventory loaded, total items: " + inventoryItems.Count);
    }
}

[Serializable]
public class ItemDataSerializable
{
    public string itemName;
    public string itemDescription;
    public int itemQuantity;
    public string iconPath;
    public int id;

    public ItemDataSerializable(ItemData itemData)
    {
        itemName = itemData.itemName;
        itemDescription = itemData.itemDescription;
        itemQuantity = itemData.itemQuantity;
        iconPath = itemData.itemIcon != null ? itemData.itemIcon.name : string.Empty;
        id = itemData.itemID;
    }

    // Method to convert Serializable data back to ItemData
    public ItemData ToItemData()
    {
        ItemData itemData = ScriptableObject.CreateInstance<ItemData>();
        itemData.InitializeItem(itemName, itemDescription, LoadIcon(iconPath), itemQuantity, id);
        return itemData;
    }

    private Sprite LoadIcon(string iconName)
    {
        return Resources.Load<Sprite>("Icons/" + iconName);
    }
}



