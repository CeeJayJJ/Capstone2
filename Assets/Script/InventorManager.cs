using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance
    {
        get; private set;
    }

    // Use a List for dynamic item storage
    private List<ItemData> inventoryItems = new List<ItemData>();

    void Awake()
    {
        // Singleton implementation (similar to other core scripts)
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
    }

    public void AddItem(ItemData item)
    {
        inventoryItems.Add(item);
        // Optionally, trigger UI update or other events
    }

    public void RemoveItem(ItemData item)
    {
        inventoryItems.Remove(item);
        // Optionally, trigger UI update or other events
    }

    public bool HasItem(ItemData item)
    {
        return inventoryItems.Contains(item);
    }

    public List<ItemData> GetInventoryItems()
    {
        return inventoryItems;
    }

    // ... (Other inventory management methods as needed)
}

