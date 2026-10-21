using UnityEngine;

public class HeldObjectPlacementScript : MonoBehaviour
{
    [SerializeField] PickUpScript pickUpScript;
    [SerializeField] Transform ItemPlacementPosition; //Empty gameobject in the scene used to determine where placed items spawn on the workstation.


    private GameObject itemOnTable; //Could've used boolean too but I saw similar method on stack overflow.
    public void PlaceHeldObject()
    {
        GameObject heldObject = pickUpScript.GetHeldObject();
        //Early exit from function
        if (heldObject == null)
        {
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

        heldObject.layer = 0; //Clears from layer holdLayer

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
}
