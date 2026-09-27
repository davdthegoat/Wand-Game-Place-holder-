using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour

{

    // just remove the whole thing vro
    [Header("Prefabs")]
    [SerializeField]
    GameObject uiItemPrefab;

    [Header("References")]
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    Transform uiInventoryParent;

    [Header("State")]
    [Header("Serialize Field")]
    SerializedDictionary<string, GameObject> inventoryUI = new();

    public void AddUIItem(string inventoryId, Item item)
    {
        var itemUI = Instantiate(uiItemPrefab).GetComponent<ItemUI>();
        itemUI.transform.SetParent(uiInventoryParent);
        inventoryUI.Add(inventoryId, itemUI.gameObject);
        itemUI.Initialize(inventoryId, item, inventory.DropItem);
    }

    public void RemoveUIItem(string inventoryId)
    {
        var itemUI = inventoryUI.GetValueOrDefault(inventoryId);
        inventoryUI.Remove(inventoryId);
        Destroy(itemUI);
    }
}
