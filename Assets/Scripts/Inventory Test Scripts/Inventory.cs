using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;



//[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{

    
    
    // [Header("Prefabs")]
    // [SerializeField]
    // GameObject droppedItemPrefab;

    

    

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Wand> inventory = new();

    [Header("WandBox Dict")]
    [SerializeField]
    SerializedDictionary<string, WandBox> boxinventory = new();



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




   


    //The actual Good stuff is here : look below
    public void AddItem(Wand item)
    {
        var inventoryId = item.id.ToString();//generate new id to allow for multiple instaces of a single item
        inventory.Add(inventoryId, item); //add it to inventory dictionary along with its ID as key
        
    }

    public void AddItemBox(WandBox itemBox)
    {
        var inventoryId = itemBox.id.ToString();//generate new id to allow for multiple instaces of a single item
        boxinventory.Add(inventoryId, itemBox); //add it to inventory dictionary along with its ID as key
        
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

    public void RemoveItemBox(string inventoryBoxId)
    {
        //creates + initiallises a new item from the prefab
        
        
        var item = boxinventory.GetValueOrDefault(inventoryBoxId);
        
        
        //Updates inventory
        boxinventory.Remove(inventoryBoxId); // removes that item from dictionary
    }

    public Wand GetItem(string inventoryId)
    {
        var item = inventory.GetValueOrDefault(inventoryId);
        return item;
    }

    public WandBox GetItemBox(string inventoryBoxId)
    {
        var itembox = boxinventory.GetValueOrDefault(inventoryBoxId);
        return itembox;
    }

    public int Length()
    {
        return inventory.Count;
    }

    public int LengthBox()
    {
        return boxinventory.Count;
    }

    public List<Wand> ReturnWandsCore(Wand.CoreType coreType)
    {
        List<Wand> Wands = new List<Wand>();
        foreach (Wand wand in inventory.Values)
        {
            if ( wand.core == coreType)
            {
                Wands.Add(wand);
            }
        }

        return Wands;
    }

    public List<Wand> ReturnWandsWood(Wand.WoodType woodType)
    {
        List<Wand> Wands = new List<Wand>();
        foreach (Wand wand in inventory.Values)
        {
            if ( wand.wood == woodType)
            {
                Wands.Add(wand);
            }
        }

        return Wands;
    }


    public void ClearInventory()
    {
        inventory.Clear();
    }

    public void ClearInventoryBox()
    {
        boxinventory.Clear();
    }


}
