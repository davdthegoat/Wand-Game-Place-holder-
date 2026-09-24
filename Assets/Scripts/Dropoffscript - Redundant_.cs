using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffScript : MonoBehaviour
{
    public GameObject player;
    public Transform storePos;
    //if you copy from below this point, you are legally required to like the video
    
    public float pickUpRange = 500f; //how far the player can pickup the object from
    
    private GameObject heldObj; //object which we pick up
    private GameObject ShelfObj;//object where held obj is stored
    private Rigidbody heldObjRb; //rigidbody of object we pick up
    private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
    private int LayerNumber; //layer index
    [Header("Equipped Slot")]
    public Transform HeldPos;
    
    
    //Reference to script which includes mouse movement of player (looking around)
    //we want to disable the player looking around when rotating the object
    //example below 
    //public Movement mouseLookScript;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("storeLayer"); //if your holdLayer is named differently make sure to change this ""
        //finding the object in the held position
        heldObj = HeldPos.GetComponentInChildren<GameObject>();
        //mouseLookScript = player.GetComponent<MouseLookScript>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
        {
            
            if (heldObj != null) //if currently holding something
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))//, pickUpRange))
                {
                    
                    //make sure pickup tag is attached
                    if (hit.transform.gameObject.tag == "canStore")
                    {
                        //pass in object hit into the PickUpObject function
                        StoreObject(hit.transform.gameObject);
                        
                        
                    }
                }
            }
            
        }
       
    }
    void StoreObject(GameObject shelf)
    {
        if (shelf.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            heldObj = shelf; //assign heldObj to the object that was hit by the raycast (no longer == null)
            heldObjRb = shelf.GetComponent<Rigidbody>(); //assign Rigidbody
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = ShelfObj.transform; //parent object to holdposition
            heldObj.layer = LayerNumber; //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void RetrieveObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; //object assigned back to default layer
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null; //unparent object
        heldObj = null; //undefine game object
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
}
