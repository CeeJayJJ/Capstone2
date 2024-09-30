using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    // Use a List for dynamic item storage
    private List<ItemData> inventoryItems = new List<ItemData>();

    void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Load inventory data from save file or initialize if new game
        LoadInventory();
    }

    public void AddItem(ItemData item)
    {
        inventoryItems.Add(item);
        Debug.Log("Item added: " + item.itemName); // Access itemName field
    }

    public void RemoveItem(ItemData item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            Debug.Log("Item removed: " + item.itemName); // Access itemName field
        }
        else
        {
            Debug.LogWarning("Item not found in inventory: " + item.itemName); // Access itemName field
        }
    }

    public bool HasItem(ItemData item)
    {
        return inventoryItems.Contains(item);
    }

    public List<ItemData> GetInventoryItems()
    {
        return inventoryItems;
    }

    // Save the inventory data
    public void SaveInventory()
    {
        SaveLoadManager.Save(inventoryItems, "Inventory");
    }

    // Load the inventory data
    public void LoadInventory()
    {
        if (SaveLoadManager.FileExists("Inventory"))
        {
            inventoryItems = SaveLoadManager.Load<List<ItemData>>("Inventory");
            Debug.Log("Inventory loaded successfully.");
        }
        else
        {
            Debug.Log("No inventory save file found, starting with empty inventory.");
        }
    }
}


