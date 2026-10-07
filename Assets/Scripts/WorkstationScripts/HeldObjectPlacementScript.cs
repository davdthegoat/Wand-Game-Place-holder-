using UnityEngine;

public class HeldObjectPlacementScript : MonoBehaviour
{
    [SerializeField] PickUpScript pickUpScript;
    [SerializeField] Transform itemPlacementPosition; //Empty gameobject in the scene used to determine where placed items spawn on the workstation.

    public void PlaceHeldObject()
    {
        GameObject heldObject = pickUpScript.GetHeldObject();
        //Early exit from function
        if (heldObject == null)
        {
            return;
        }

        Rigidbody heldObjectRb = pickUpScript.GetHeldObjectRigidbody();

        heldObject.transform.parent = null; //Unparent from gameobject heldPos

        //Move to desired spot
        heldObject.transform.position = itemPlacementPosition.position;
        heldObject.transform.rotation = itemPlacementPosition.rotation;

        heldObject.layer = 0; //Clears from layer holdLayer

        heldObjectRb.isKinematic = true; //prevents physics interactin so item can't be moved/nudged off table

        pickUpScript.ClearHeldObject();
    }
}
