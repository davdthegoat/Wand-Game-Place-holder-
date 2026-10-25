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
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();

        if (BoxwandData != null)
        {
            string wandBoxItem = BoxwandData.wandboxitem.id;
            playerInventory.RemoveItemBox(wandBoxItem);
        }
        else if (wandData != null)
        {
            string wandItem = wandData.item.id;
            playerInventory.RemoveItem(wandItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotRemoveItem?.Invoke();}//Cant remove from inv if its not a WandBox or wand
    }

    void AddItem(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();

        if ( wandData != null)
        {
            Wand wandItem = wandData.item;
            playerInventory.AddItem(wandItem);
        }
        else if ( BoxwandData != null)
        {
            WandBox wandBoxItem = BoxwandData.wandboxitem;
            playerInventory.AddItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotAddItem?.Invoke();} //cant add item to inv if its not Wand or WandBox
        
    }

    void AddItemGroundInventory(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();
        if (wandData != null)
        {
            Wand wandItem = wandData.item;
            GroundInventory.AddItem(wandItem);
        }
        else if (BoxwandData != null)
        {
            WandBox wandBoxItem = BoxwandData.wandboxitem;
            GroundInventory.AddItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotAddItem?.Invoke();}
    }

    void RemoveItemGroundInventory(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();
        if (wandData != null)
        {
            string wandItem = wandData.item.id;
            GroundInventory.RemoveItem(wandItem);
        }
        else if (BoxwandData != null)
        {
            string wandBoxItem = BoxwandData.wandboxitem.id;
            GroundInventory.RemoveItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotRemoveItem?.Invoke();} 
    }

    void AddItemShelfInventory(GameObject item, Inventory GridInventory)
    {
        //Only allow WandBox to be stored in the shelf
        var wandBoxData = item.GetComponent<WandBoxItem>();
        if (wandBoxData != null)
        {
            var wandBoxItem = wandBoxData.wandboxitem;
            GridInventory.AddItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotStoreInShelf?.Invoke();} //Cannot store anything other than the WandBox in a shelf
    }

    void RemoveItemShelfInventory(GameObject item, Inventory GridInventory)
    {
        var wandBoxData = item.GetComponent<WandBoxItem>();
        if (wandBoxData != null)
        {
            var wandBoxItemid = wandBoxData.wandboxitem.id;
            GridInventory.RemoveItemBox(wandBoxItemid);
        }
    }

    void FindItembyCore()
    {
        ;
    }




    /* DVD - Commenting out cuz of null error
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
    */
}
