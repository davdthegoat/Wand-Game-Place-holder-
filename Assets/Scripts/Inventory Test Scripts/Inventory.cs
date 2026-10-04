using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;



//[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{

    public static Inventory InventorySingleton;
    
    [Header("Prefabs")]
    [SerializeField]
    GameObject droppedItemPrefab;

    

    

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Wand> inventory = new();



    //script to pick up a dropped item on trigger enter
    //modify to work with your other script of raycast


    /// <summary>
    /// Store Item in Inventory
    /// </summary>
    /// 
    /// Shift to pickup script pls pls pls pls pls pls pls pls pls pls pls

    // public void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("DroppedItem"))
    //     {
    //         var droppedItem = other.GetComponent<DroppedItem>(); // get the dropped item script attacked to a component with a dropped item tag
    //         if (droppedItem.pickedUp) //if the pickedm up variable is true 
    //         {
    //             return; //dont pick up again go out of function
    //         }
    //         droppedItem.pickedUp = true; // else picked up is true set
    //         AddItem(droppedItem.item); // Added item to the inventory
    //         Destroy(other.gameObject); // Removed the gameobject from the scene

    //     }
    // }




    void Start()
    {
        InventorySingleton = this;
    }



    //The actual Good stuff is here : look below
    public void AddItem(Wand item)
    {
        var inventoryId = item.id.ToString();//generate new id to allow for multiple instaces of a single item
        inventory.Add(inventoryId, item); //add it to inventory dictionary along with its ID as key
        
    }

    // public void DropItem(string inventoryId)
    // {
    //     var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
    //     var item = inventory.GetValueOrDefault(inventoryId);
    //     droppedItem.Initialize(item);
    //     inventory.Remove(inventoryId);
    // }

    //Remove Item from Inventory
    public void RemoveItem(string inventoryId)
    {
        //creates + initiallises a new item from the prefab
        
        
        var item = inventory.GetValueOrDefault(inventoryId);
        
        
        //Updates inventory
        inventory.Remove(inventoryId); // removes that item from dictionary
        

        
        

    }


    public void ClearInventory()
    {
        inventory.Clear();
    }

}
