using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public struct ShelfEvents
{
    public Action<float> BimbimBamBam;
    public Action<string> RemoveItemInventory;

    
    public Action<Wand> AddItemInventory;
}
