using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryManager : PlayerSystem
{


    [SerializeField]
    private Inventory playerInventory;


    void RemoveItem(string id)
    {
        playerInventory.RemoveItem(id);
    }

    void AddItem(Wand wandItem)
    {
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
