using UnityEngine;

public class HeldObjectPlacementScript : MonoBehaviour
{
    [SerializeField] PickUpScript pickUpScript;
    [SerializeField] Transform ItemPlacementPosition; //Empty gameobject in the scene used to determine where placed items spawn on the workstation.
    [Header ("Powerups")]
    [SerializeField] FloatPowerup floatPowerup;
    [SerializeField] GameObject readableParchment;


    private GameObject itemOnTable; //Could've used boolean too but I saw similar method on stack overflow.
    public void PlaceHeldObject()
    {
        GameObject heldObject = pickUpScript.GetHeldObject();
        //Early exit from function
        if (heldObject == null)
        {
            return;
        }
        if (heldObject.name == "PowerupParchment-Float") //If the float powerup is brought to the workbench. We don't actually want to place it on the table.
        {
            Debug.Log("PowerUp brought to workstation");
            floatPowerup.enabled = true;
            readableParchment.SetActive(true); //The parchment. Can go with two approaches. I like approach I more than II, but it comes down to time investment.
            // I: A differently modelled parchment for each powerup, to give it uniqueness. Perhaps instead of differently modelled, it can be differently textured, what I'm trying to say is that it should look different
            // II: Same parchment, but depending on which powerup is brought to the workstation, a different TMP (3D) is enabled. This is a temporary/cheap fix, but it'll get the job done. It'd look something like the following line:
            // floatPowerupPopUpText.SetActive(true);, where floatPowerupPopUpText is a serialized TMP.
            //Possibly destroy the gameobject. Unsure, gameplay choice.
            return;
        }
        if (itemOnTable != null)
        {
            //Making it so that instead of dropping the item, the item is just not visible to sit position camera. Remains in players hand, when exiting workstation.
            //DropHeldObject(heldObject); //Not the same from pickup script, slight modifications for workstation only. That one is called DropObject. In particular, the real difference is inventory.
            return; //Meaning the rest of the code is skipped.
        }
        Rigidbody heldObjectRb = pickUpScript.GetHeldObjectRigidbody();

        heldObject.transform.parent = null; //Unparent from gameobject heldPos

        //Move to desired spot
        heldObject.transform.position = ItemPlacementPosition.position;
        heldObject.transform.rotation = ItemPlacementPosition.rotation;

        SetLayerChildren(heldObject, LayerMask.NameToLayer("WorkStationDrag")); //Clears from layer holdLayer

        heldObjectRb.isKinematic = true; //prevents physics interactin so item can't be moved/nudged off table

        itemOnTable = heldObject;
        pickUpScript.ClearHeldObject(); //All prior steps necessary because ClearHeldObject only sets heldObj and heldObjRb to null.


        //Giving it a tag, for workstation WorkStationDragAndDrop script.
        //Revoking tag. Item should be pick-up-able even after placed on table and cleared.
        heldObject.tag = "canPickUp";
    }

    public GameObject GetItemOnTable()
    {
        return itemOnTable;
    }

    public void ClearItemOnTable(GameObject item)
    {
        if (itemOnTable == item)
        {
            itemOnTable = null;
        }
    }

    void SetLayerChildren( GameObject objparent, LayerMask layer)
    {
        if (objparent == null)
        {
            return;
        }
        
        
        objparent.layer = layer;
        foreach (Transform child in objparent.transform)
        {
            child.gameObject.layer = layer;
        }

    }

    public void DisableReadableParchment()
    {
        readableParchment.SetActive(false);
    }
}
