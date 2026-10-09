using System;
using System.Collections.Generic;
using UnityEngine;

public class InitGroundInventory : MonoBehaviour
{
    [SerializeField]
    Inventory groundInventory;

    //List of all Wand objects in the Game world
    public List<DroppedItem> WandsToInit = new List<DroppedItem>();
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (DroppedItem wand in WandsToInit)
        {
            groundInventory.AddItem(wand.item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
