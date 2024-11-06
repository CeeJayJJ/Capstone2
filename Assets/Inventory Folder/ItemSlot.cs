using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{

    //ItemData slot//
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] 
    private Mesh itemMesh; // Assign a 3D model in the inspector
    [SerializeField] 
    private Material itemMaterial; // Assign a material in the inspector

    [SerializeField]
    private int maxNumberOfItems;

    //Item Slot//
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    //Item Description Slot//
    public Image itemmDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private ItemManager1 itemManager1;

    void Start()
    {
        itemManager1 = GameObject.Find("Canvas").GetComponent<ItemManager1>();
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription) 
    {
        //check if the slot stack is full
        if (isFull)
            return quantity;

        //update name
        this.itemName = itemName;
        
        //update image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        //update description
        this.itemDescription = itemDescription; 

        //update quantity
        this.quantity += quantity;

        if(this.quantity >= maxNumberOfItems)
        {
          quantityText.text = maxNumberOfItems.ToString();
          quantityText.enabled = true;
          isFull = true;

        //return left overs
        int extraItems = this.quantity - maxNumberOfItems;
        this.quantity = maxNumberOfItems;
        return extraItems;
        }

        //update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        if (thisItemSelected)
        {
            bool usable = itemManager1.UseItem(itemName);
            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();

                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }
        }
        else
        {
            itemManager1.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemmDescriptionImage.sprite = itemSprite != null ? itemSprite : emptySprite;
        }
    }

    private void EmptySlot()
    {
     quantityText.enabled = false;
     itemImage.sprite = emptySprite;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemmDescriptionImage.sprite = emptySprite;

        isFull = false;
        itemName = string.Empty;
        quantity = 0;
        itemSprite = emptySprite;

        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        itemDescriptionNameText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
        itemImage.sprite = emptySprite;

        selectedShader.SetActive(false);
        thisItemSelected = false;

    }

    private void OnRightClick()
    {
        // Create a new item GameObject
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        // Create and modify the 3D Mesh Renderer
        MeshRenderer mr = itemToDrop.AddComponent<MeshRenderer>();
        MeshFilter mf = itemToDrop.AddComponent<MeshFilter>();

        // Assign the mesh and material for 3D visualization
        mf.mesh = itemMesh; // Assign your 3D item mesh in the inspector or code
        mr.material = itemMaterial; // Assign your material in the inspector or code

        // Add a 3D Box Collider for physical interaction
        BoxCollider collider = itemToDrop.AddComponent<BoxCollider>();
        collider.size = new Vector3(1f, 1f, 1f);  // Adjust collider size if needed

        // Add a Rigidbody for physics and apply initial force
        Rigidbody rb = itemToDrop.AddComponent<Rigidbody>();
        rb.mass = 0.5f; // Adjust mass for desired item weight
        rb.useGravity = true; // Enable gravity
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Better for fast-moving objects

        // Set the position near the player with an offset for height and distance
        Transform playerTransform = GameObject.FindWithTag("Player").transform;
        itemToDrop.transform.position = playerTransform.position + playerTransform.forward + new Vector3(0, 0, 0.5f);  // Position slightly above the ground

        // Apply a forward "toss" force for a slight throwing effect
        Vector3 tossDirection = (playerTransform.forward + new Vector3(0, 0, 0.3f)).normalized; // Adds a slight upward angle
        rb.AddForce(tossDirection * 3f, ForceMode.Impulse); // Adjust the force multiplier as desired

        // Scale the item to an appropriate size
        itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        // Subtract the item from the inventory
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }
}
