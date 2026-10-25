
using System.Collections.Generic;
using UnityEngine;



public class PickUpScript : PlayerSystem
{
    [SerializeField] private HeldObjectPlacementScript heldObjectPlacementScript;
    public GameObject player;
    public Transform holdPos;
    //if you copy from below this point, you are legally required to like the video
    public float throwForce = 500f; //force at which the object is thrown at
    public float pickUpRange = 500f; //how far the player can pickup the object from
    private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
    private GameObject heldObj; //object which we pick up
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index
    private int LayerNumStore;

    
    

    
    //Reference to script which includes mouse movement of player (looking around)
    //we want to disable the player looking around when rotating the object
    //public Movement mouseLookScript;
    private Movement movement;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""
        LayerNumStore = LayerMask.NameToLayer("storeLayer");
        //mouseLookScript = player.GetComponent<MouseLookScript>();
        movement = player.GetComponent<Movement>();
        
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            if (heldObj == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //make sure item is of a type that can be picked up
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        //pass in object hit into the PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                }
            }
        }

        if (heldObj != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos
            RotateObject();

            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                StopClipping();
                ThrowObject();
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjectPlacementScript?.ClearItemOnTable(pickUpObj);
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.useGravity = false;
            heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
            SetLayerChildren(heldObj, LayerNumber); //change the object layer to the holdLayer //LAYER

            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            StoredGridItem storedGridItem = pickUpObj.GetComponent<StoredGridItem>();
            if (storedGridItem != null && storedGridItem.testingGrid != null)
            {
                storedGridItem.testingGrid.GetGrid().SetGridValue(storedGridItem.gridX, storedGridItem.gridY, 0);

                
                var takeoutItem = heldObj;
                Inventory GridInventory = storedGridItem.testingGrid.GetComponent<Inventory>();
                if (GridInventory != null || takeoutItem != null){
                playerIdentification.Playerdata.Events.RemoveItemShelfInventory?.Invoke(takeoutItem, GridInventory);
                //PUT into player inventory
                }
                
                
               

                storedGridItem.testingGrid = null;
            }

           /*
            var droppedItem = heldObj;
            if (droppedItem != null) {playerIdentification.Playerdata.Events.AddItemInventory?.Invoke(droppedItem); //This line is causing problems. (null references)
            playerIdentification.Playerdata.Events.RemoveItemGroundInventory?.Invoke(droppedItem);}
           */
           
        }
    }

    public void DropObject()
    {
        Debug.Log("DROP OBJECT WAS CALLED");
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        SetLayerChildren(heldObj, 0); //object assigned back to default layer
        
        //DROPINVENTROY
        var dropitem = heldObj;
        if (dropitem == null) Debug.Log("DROPITEM IS NULL GOING TO REMOVEITEMINVENTORY - DROPOBJECT");
        else{
        playerIdentification.Playerdata.Events.RemoveItemInventory?.Invoke(dropitem);
        //DROPINVENTORY
        playerIdentification.Playerdata.Events.AddItemGroundInventory?.Invoke(dropitem);
        }

        heldObjRb.isKinematic = false;
        heldObjRb.useGravity = true;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
        heldObjRb = null;
       
    }

    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        heldObj.transform.position = holdPos.transform.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around
            movement.SetCameraLocked(true);

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

            //rotate the object depending on mouse X-Y Axis
            heldObj.transform.Rotate(Vector3.down, XaxisRotation);
            heldObj.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            //re-enable player being able to look around
            movement.SetCameraLocked(false);
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        Debug.Log("ThrowObject was called");
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        SetLayerChildren(heldObj, 0);
        heldObjRb.isKinematic = false;
        heldObjRb.useGravity = true;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        

        //TESTING, REMOVING THIS.
        
        //INVENTORY
        var dropitem = heldObj;
        if (dropitem == null) Debug.Log("DROPITEM IS NULL GOING TO REMOVEITEMINVENTORY - THROW OBJECT");
        else{
        playerIdentification.Playerdata.Events.RemoveItemInventory?.Invoke(dropitem);
        //DROPINVENTORY
        //Put in GROUND INVENTORY
        playerIdentification.Playerdata.Events.AddItemGroundInventory?.Invoke(dropitem);
        }
        

        heldObj = null;
        heldObjRb = null;

       
    }

    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera

        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    public GameObject GetHeldObject()
    {
        return heldObj;
    }

    public Rigidbody GetHeldObjectRigidbody()
    {
        return heldObjRb;
    }

    public void ClearHeldObject()
    {
        Debug.Log("ClearHeldObject was called");

        //DVD here, disabling for testing purposes.
        /*
        //INVENTORY
        var droppedItem = heldObj.GetComponent<DroppedItem>();
        inventory.RemoveItem(droppedItem.item.id);
        //INVENTORY
        */
        //This code above keeps giving null reference.
        


        heldObj = null;
        heldObjRb = null;
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

    
}