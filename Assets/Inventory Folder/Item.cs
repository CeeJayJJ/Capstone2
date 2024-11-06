using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite sprite;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    private ItemManager1 inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("Canvas")?.GetComponent<ItemManager1>();
        if (inventoryManager == null)
        {
            Debug.LogWarning("ItemManager1 component is missing on the Canvas.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (inventoryManager != null)
            {
                int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

                if (leftOverItems <= 0)
                    Destroy(gameObject);
                else
                    quantity = leftOverItems;
            }
            else
            {
                Debug.LogWarning("InventoryManager is not assigned.");
            }
        }
    }
}
