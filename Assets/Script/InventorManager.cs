using UnityEngine;
using System.Collections.Generic;

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
