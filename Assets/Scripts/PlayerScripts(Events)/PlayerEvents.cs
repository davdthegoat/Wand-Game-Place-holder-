using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public struct PlayerEvents
{
    public Action CannotStoreInShelf;

    public Action CannotAddItem;
    public Action CannotRemoveItem;

    public Action<GameObject> RemoveItemInventory;
    public Action<GameObject> AddItemInventory;

    public Action<GameObject> AddItemGroundInventory;
    public Action<GameObject> RemoveItemGroundInventory;

    public Action<GameObject, Inventory> AddItemShelfInventory;
    public Action<GameObject, Inventory> RemoveItemShelfInventory;
}
