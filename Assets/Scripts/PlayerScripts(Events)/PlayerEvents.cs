using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public struct PlayerEvents
{
    public Action<float> BimbimBamBam;
    public Action<GameObject> RemoveItemInventory;

    
    public Action<GameObject> AddItemInventory;

    public Action<GameObject> AddToGroundInventory;

    public Action<GameObject> RemoveFromGroundInventory;
}
