using UnityEngine;

public class HeldObjectPlacementScript : MonoBehaviour
{
    [SerializeField] PickUpScript pickUpScript;
    [SerializeField] Transform ItemPlacementPosition; //Empty gameobject in the scene used to determine where placed items spawn on the workstation.
    [SerializeField] Transform DroppedItemPlacementPosition;

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
            DropHeldObject(heldObject); //Not the same from pickup script, slight modifications for workstation only. That one is called DropObject. In particular, the real difference is inventory.
            return;
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
    }

    private void DropHeldObject(GameObject heldObject)
    {
        heldObject.transform.position = DroppedItemPlacementPosition.position;
        heldObject.transform.rotation = DroppedItemPlacementPosition.rotation;
        //The code above is a QOL feature. Otherwise, depending on player position item might still get dropped onto the table.
        pickUpScript.DropObject();
        //At first I tried to create a modified version of DropObject, which didn't involve inventory. But the inventory is an essential part of the game and this function can't be completed without it.
    }
}
