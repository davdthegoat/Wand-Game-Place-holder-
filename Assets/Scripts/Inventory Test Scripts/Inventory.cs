using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;
using UnityEditor.Compilation;
using Unity.VisualScripting;

//[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    
    

    

    

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
    
    
    

    //The actual Good stuff is here : look below
    void AddItem(Wand item)
    {
        var inventoryId = Guid.NewGuid().ToString();//generate new id to allow for multiple instaces of a single item
        inventory.Add(inventoryId, item); //add it to inventory dictionary along with its ID as key
        // ui.AddUIItem(inventoryId, item); //add it to ui
    }



    //Remove Item from Inventory
    public void RemoveItem(string inventoryId)
    {
        //creates + initiallises a new item from the prefab
        
        
        var item = inventory.GetValueOrDefault(inventoryId);
        
        
        //Updates inventory
        inventory.Remove(inventoryId); // removes that item from dictionary
        

        
        

    }

}
