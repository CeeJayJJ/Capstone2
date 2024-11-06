using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager1 : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSO;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string itemName)
    {
        for(int i = 0; i < itemSO.Length; i++)
        {
            if (itemSO[i].itemName == itemName)
            {
                bool usable = itemSO[i].UseItem();
                return usable;
            }
            
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i <= itemSlot.Length ; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[1].name == name || itemSlot[i].quantity == 0)
            {
               int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
               
                if(leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                return leftOverItems;
            }
        }

        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
