using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventoryManager : PlayerSystem
{


    [SerializeField]
    private Inventory playerInventory;


    void RemoveItem(GameObject item)
    {   
        if (NotNullDroppedItem(item))
        {
            var id = item.GetComponent<DroppedItem>().item.id; 
            playerInventory.RemoveItem(id);
        }
    }

    void AddItem(GameObject item)
    {   
        if (NotNullDroppedItem(item)) // Ensures getting the item doesnt return null
        {
            var wandItem = item.GetComponent<DroppedItem>().item; 
            playerInventory.AddItem(wandItem);
        }
    }

    void AddItemToGroundInventory(GameObject item)
    {
        
    }




    private bool NotNullDroppedItem(GameObject item)
    {
        return item.GetComponent<DroppedItem>() != null;
         // Ensures getting the item doesnt return null
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
