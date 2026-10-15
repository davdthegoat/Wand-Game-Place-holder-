using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryManager : PlayerSystem
{


    [SerializeField]
    private Inventory playerInventory;
    [SerializeField]
    private Inventory GroundInventory;


    void RemoveItem(GameObject item)
    {
        if (item.GetComponent<DroppedItem>() != null)
        {
            string wandItem = item.GetComponent<DroppedItem>().item.id;
            playerInventory.RemoveItem(wandItem);
        }
    }

    void AddItem(GameObject item)
    {
        if (item.GetComponent<DroppedItem>() != null)
        {
            Wand wandItem = item.GetComponent<DroppedItem>().item;
            playerInventory.AddItem(wandItem);
        }
    }

    void AddItemGroundInventory(GameObject item)
    {
          if (item.GetComponent<DroppedItem>() != null)
        {
            Wand wandItem = item.GetComponent<DroppedItem>().item;
            GroundInventory.AddItem(wandItem);
        }
    }

    void RemoveItemGroundInventory(GameObject item)
    {
          if (item.GetComponent<DroppedItem>() != null)
        {
            string wandItem = item.GetComponent<DroppedItem>().item.id;
            GroundInventory.RemoveItem(wandItem);
        }
    }

    void AddItemShelfInventory(GameObject item, Inventory GridInventory)
    {
        if (item.GetComponent<DroppedItem>() != null)
        {
            var wandItem = item.GetComponent<DroppedItem>().item;
            GridInventory.AddItem(wandItem);
        }
    }

    void RemoveItemShelfInventory(GameObject item, Inventory GridInventory)
    {
        if (item.GetComponent<DroppedItem>() != null)
        {
            var wandItemid = item.GetComponent<DroppedItem>().item.id;
            GridInventory.RemoveItem(wandItemid);
        }
    }






    void OnDisable()
    {
        playerIdentification.Playerdata.Events.AddItemInventory -= AddItem;
        playerIdentification.Playerdata.Events.RemoveItemInventory -= RemoveItem; 

        playerIdentification.Playerdata.Events.RemoveItemGroundInventory -= RemoveItemGroundInventory;
        playerIdentification.Playerdata.Events.AddItemGroundInventory -= AddItemGroundInventory;

        playerIdentification.Playerdata.Events.AddItemShelfInventory -= AddItemShelfInventory;
        playerIdentification.Playerdata.Events.RemoveItemShelfInventory -= RemoveItemShelfInventory;

    }
    void OnEnable()
    {
        //Subscribing these functions to neccassary actions triggered in pickup script rn
        playerIdentification.Playerdata.Events.AddItemInventory += AddItem;
        playerIdentification.Playerdata.Events.RemoveItemInventory += RemoveItem;

        playerIdentification.Playerdata.Events.RemoveItemGroundInventory += RemoveItemGroundInventory;
        playerIdentification.Playerdata.Events.AddItemGroundInventory += AddItemGroundInventory; 

        playerIdentification.Playerdata.Events.AddItemShelfInventory += AddItemShelfInventory;
        playerIdentification.Playerdata.Events.RemoveItemShelfInventory += RemoveItemShelfInventory;
    }
}
