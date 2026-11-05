using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : PlayerSystem
{


    [SerializeField]
    private Inventory playerInventory;
    [SerializeField]
    private Inventory GroundInventory;

    [Header("TEXT REFERENCE")]
    [SerializeField] private TextMeshProUGUI Coretext;
    [SerializeField] private TextMeshProUGUI Woodtext;
    [SerializeField] private TextMeshProUGUI Lengthtext;

    


    void RemoveItem(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();

        if (BoxwandData != null)
        {
            string wandBoxItem = BoxwandData.wandboxitem.id;
            playerInventory.RemoveItemBox(wandBoxItem);
        }
        else if (wandData != null)
        {
            string wandItem = wandData.item.id;
            playerInventory.RemoveItem(wandItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotRemoveItem?.Invoke();}//Cant remove from inv if its not a WandBox or wand
    }

    void AddItem(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();

        if ( wandData != null)
        {
            Wand wandItem = wandData.item;
            playerInventory.AddItem(wandItem);
        }
        else if ( BoxwandData != null)
        {
            WandBox wandBoxItem = BoxwandData.wandboxitem;
            playerInventory.AddItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotAddItem?.Invoke();} //cant add item to inv if its not Wand or WandBox
        
    }

    void AddItemGroundInventory(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();
        if (wandData != null)
        {
            Wand wandItem = wandData.item;
            GroundInventory.AddItem(wandItem);
        }
        else if (BoxwandData != null)
        {
            WandBox wandBoxItem = BoxwandData.wandboxitem;
            GroundInventory.AddItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotAddItem?.Invoke();}
    }

    void RemoveItemGroundInventory(GameObject item)
    {
        var wandData = item.GetComponent<DroppedItem>();
        var BoxwandData = item.GetComponent<WandBoxItem>();
        if (wandData != null)
        {
            string wandItem = wandData.item.id;
            GroundInventory.RemoveItem(wandItem);
        }
        else if (BoxwandData != null)
        {
            string wandBoxItem = BoxwandData.wandboxitem.id;
            GroundInventory.RemoveItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotRemoveItem?.Invoke();} 
    }

    void AddItemShelfInventory(GameObject item, Inventory GridInventory)
    {
        //Only allow WandBox to be stored in the shelf
        var wandBoxData = item.GetComponent<WandBoxItem>();
        if (wandBoxData != null)
        {
            var wandBoxItem = wandBoxData.wandboxitem;
            GridInventory.AddItemBox(wandBoxItem);
        }
        else{ playerIdentification.Playerdata.Events.CannotStoreInShelf?.Invoke();} //Cannot store anything other than the WandBox in a shelf
    }

    void RemoveItemShelfInventory(GameObject item, Inventory GridInventory)
    {
        var wandBoxData = item.GetComponent<WandBoxItem>();
        if (wandBoxData != null)
        {
            var wandBoxItemid = wandBoxData.wandboxitem.id;
            GridInventory.RemoveItemBox(wandBoxItemid);
        }
    }

    void FindItembyCore()
    {
        ;
    }
    //The Text Mesh Displays
    void DisplayCore(GameObject heldObj)
    {   
        var wandData = heldObj.GetComponent<DroppedItem>().item;
        if (wandData == null){return;}
        else
        {
            var coreEnum = wandData.core;
            string coreold = coreEnum.ToString();
            string core = "";
            foreach (char letter in coreold)
            {
                if (letter== '_')
                {
                    core += " ";
                }
                else{core+= letter;}
            }
            core = "Core: "+core;
            Coretext.text = core; 
        }
    }
    void DisplayWood(GameObject heldObj)
    {
        var wandData = heldObj.GetComponent<DroppedItem>().item;
        if (wandData == null){return;}
        else
        {
            var woodEnum = wandData.wood;
            string wood = woodEnum.ToString();
            wood = "Wood: " + wood;
            Woodtext.text = wood; 
        }
    }
    void DisplayLength(GameObject heldObj)
    {
        var wandData = heldObj.GetComponent<DroppedItem>().item;
        if (wandData == null){return;}
        else
        {
            var lengthEnum = wandData.length;
            string length = lengthEnum.ToString();
            length = "Length: " + length;
            Lengthtext.text = length; 
        }
    }

    void ClearTextcorewoodlen()
    {
        Coretext.text = "";
        Woodtext.text = "";
        Lengthtext.text = "";
    }

    //DVD - Commenting out cuz of null error
    void OnDisable()
    {
        // playerIdentification.Playerdata.Events.AddItemInventory -= AddItem;
        // playerIdentification.Playerdata.Events.RemoveItemInventory -= RemoveItem; 

        // playerIdentification.Playerdata.Events.RemoveItemGroundInventory -= RemoveItemGroundInventory;
        // playerIdentification.Playerdata.Events.AddItemGroundInventory -= AddItemGroundInventory;

        // playerIdentification.Playerdata.Events.AddItemShelfInventory -= AddItemShelfInventory;
        // playerIdentification.Playerdata.Events.RemoveItemShelfInventory -= RemoveItemShelfInventory;

        playerIdentification.Playerdata.Events.InteractionOccured -= DisplayCore;
        playerIdentification.Playerdata.Events.InteractionOccured -= DisplayWood;
        playerIdentification.Playerdata.Events.InteractionOccured -= DisplayLength;

        playerIdentification.Playerdata.Events.InteractionEnded -= ClearTextcorewoodlen;

    }
    void OnEnable()
    {
        //Subscribing these functions to neccassary actions triggered in pickup script rn
        // playerIdentification.Playerdata.Events.AddItemInventory += AddItem;
        // playerIdentification.Playerdata.Events.RemoveItemInventory += RemoveItem;

        // playerIdentification.Playerdata.Events.RemoveItemGroundInventory += RemoveItemGroundInventory;
        // playerIdentification.Playerdata.Events.AddItemGroundInventory += AddItemGroundInventory; 

        // playerIdentification.Playerdata.Events.AddItemShelfInventory += AddItemShelfInventory;
        // playerIdentification.Playerdata.Events.RemoveItemShelfInventory += RemoveItemShelfInventory;

        playerIdentification.Playerdata.Events.InteractionOccured += DisplayCore;
        playerIdentification.Playerdata.Events.InteractionOccured += DisplayWood;
        playerIdentification.Playerdata.Events.InteractionOccured += DisplayLength;

        playerIdentification.Playerdata.Events.InteractionEnded += ClearTextcorewoodlen;
    }

    
}
