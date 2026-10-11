using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryManager : PlayerSystem
{


    [SerializeField]
    private Inventory playerInventory;


    void RemoveItem(GameObject item)
    {   
        var id = item.GetComponent<DroppedItem>().item.id; 
        playerInventory.RemoveItem(id);
    }

    void AddItem(GameObject item)
    {
        var wandItem = item.GetComponent<DroppedItem>().item; 
        playerInventory.AddItem(wandItem);
    }








    void OnDisable()
    {
        playerIdentification.Playerdata.Events.AddItemInventory -= AddItem;
        
        playerIdentification.Playerdata.Events.RemoveItemInventory -= RemoveItem; 

    }
    void OnEnable()
    {
        //Subscribing these functions to neccassary actions triggered in pickup script rn
        playerIdentification.Playerdata.Events.AddItemInventory += AddItem;
        
        playerIdentification.Playerdata.Events.RemoveItemInventory += RemoveItem; 
    }
}
