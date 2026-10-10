using Unity.VisualScripting;
using UnityEngine;

public class ShelfInventoryManager : PlayerSystem
{


    [SerializeField]
    private Inventory shelfInventory;


    void RemoveItem(string id)
    {
        shelfInventory.RemoveItem(id);
    }

    void AddItem(Wand wandItem)
    {
        shelfInventory.AddItem(wandItem);
    }








    void OnDisable()
    {
        //ShelfIdentification.Shelfdata.Events.AddItemInventory -= AddItem;
        
        //ShelfIdentification.Shelfdata.Events.RemoveItemInventory -= RemoveItem; 

    }
    void OnEnable()
    {
        //Subscribing these functions to neccassary actions triggered in pickup script rn
       // ShelfIdentification.Shelfdata.Events.AddItemInventory += AddItem;
        
        //ShelfIdentification.Shelfdata.Events.RemoveItemInventory += RemoveItem; 
        
    }
}
