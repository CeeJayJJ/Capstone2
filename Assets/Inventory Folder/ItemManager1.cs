using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemManager1 : MonoBehaviour
{
    public GameObject inventoryMenu;
    public bool menuActivated;
    public ItemSlot[] itemSlot;

    public ItemSO[] itemSO;

    public static ItemManager1 itemManager1;

    private void Awake()
    {
        if (itemManager1 == null)
        {
            itemManager1 = this;
        }
        else if (itemManager1 != this)
        {
            Destroy(gameObject);
        }
    }
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

            if (QuestUIManager.uiManager.questLogPanel.activeSelf)
            {
                QuestUIManager.uiManager.HideQuestLogPanel();
            }
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
