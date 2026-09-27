using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Rendering;
using UnityEditor.Compilation;
using Unity.VisualScripting;

//[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    [Header("ReferencesOptions")]
    //Reference to Inventory UI class which will be the UI script for inventory
    // [SerializeField]
    // InventoryUI ui;
    //Dont think its important you can remove
    //Basically audio source attached to the player is put here
    [SerializeField]
    AudioSource audioSource;

    [Header("Prefabs")]
    [SerializeField]
    GameObject droppedItemPrefab;
    //You can remove later

    [Header("Audio Clips")]
    [SerializeField]
    AudioClip pickUpItemAudio;
    [SerializeField]
    AudioClip dropItemAudio;

    [Header("State")]
    [SerializeField]
    SerializedDictionary<string, Item> inventory = new();


    
    //script to pick up a dropped item on trigger enter
    //modify to work with your other script

    
    /// <summary>
    /// Store Item in Inventory
    /// </summary>
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DroppedItem"))
        {
            var droppedItem = other.GetComponent<DroppedItem>();
            if (droppedItem.pickedUp)
            {
                return; // if picked up alr dont do anything
            }
            //if not picked up
            droppedItem.pickedUp = true;
            AddItem(droppedItem.item); //add to inventory
            Destroy(other.gameObject); //destroy the GameObject from the scene

            //insert any effects/animations/sounds here which will play after the game object has been picked up
            audioSource.PlayOneShot(pickUpItemAudio);
        }
    }
    

    //The actual Good stuff is here : look below
    void AddItem(Item item)
    {
        var inventoryId = Guid.NewGuid().ToString();//generate new id to allow for multiple instaces of a single item
        inventory.Add(inventoryId, item); //add it to inventory dictionary along with its ID as key
        // ui.AddUIItem(inventoryId, item); //add it to ui
    }



    //Remove Item from Inventory
    public void DropItem(string inventoryId)
    {
        //creates + initiallises a new item from the prefab
        var droppedItem = Instantiate(droppedItemPrefab, transform.position, Quaternion.identity).GetComponent<DroppedItem>();
        
        var item = inventory.GetValueOrDefault(inventoryId);
        droppedItem.Initialize(item); //initialises the game object item in inventory with id inventoryId idk what this is yet
        
        //Updates inventory
        inventory.Remove(inventoryId); // removes that item from dictionary
        // ui.RemoveUIItem(inventoryId);

        //plays a sound if you wan ig
        audioSource.PlayOneShot(dropItemAudio);

    }

}
