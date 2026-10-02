using Unity.Properties;
using UnityEngine;

public class StoreInShelfScript : MonoBehaviour
{
    //Dont need player collision to be disabled when storing in shelf
    //[SerializeField] GameObject player;
    [SerializeField] float storeRange = 5f;

    private int LayerNumStore;
    private GameObject shelfObj;
    private bool inShelf_;
    private PickUpScript pickUpScript;
    private TestingGrid testingGrid;
    private Grid grid; 



    void Start()
    {
        LayerNumStore = LayerMask.NameToLayer("storeLayer");
        pickUpScript = GetComponent<PickUpScript>();

        //Debug.Log("StoreInShelfScript has found pickUpScript " + (pickUpScript != null)); No longer useful, code works.

        testingGrid = FindFirstObjectByType<TestingGrid>();
        grid = testingGrid.GetGrid();
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject heldObj = pickUpScript.GetHeldObject();

            if (heldObj == null)
            {
                return;
            }

            RaycastHit hit;

             disabled temporarily for testing purposes. 
            if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,pickUpRange))
            {
                if (hit.transform.gameObject.tag == "canStore")
                {
                    StoreInShelf(hit.transform.gameObject);
                }
            }
            
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                Debug.Log("F raycast hit " + hit.transform.gameObject.name);
                Debug.Log("F raycast hit " + hit.transform.gameObject.tag);

                if (hit.transform.gameObject.tag == "canStore")
                {
                    StoreInShelf(hit.transform.gameObject);
                }
            }
        }
    REMOVE FOR DEBUGGING, RESTORE IN FINAL VERSION OF GAME
    } */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject heldObj = pickUpScript.GetHeldObject();
            Rigidbody heldObjRb = pickUpScript.GetHeldObjectRigidbody();
            Debug.Log("F was pressed");
            Debug.Log("PickUpScript reference " + pickUpScript); 
            Debug.Log("heldObj immediately after pickup: " + heldObj);
            Debug.Log("heldObjRb immediately after pickup: " + heldObjRb);

            if (heldObj == null)
            {
                Debug.Log("F pressed, but heldObj is NULL");
                return;
            }

            Debug.Log("F pressed, held object is: " + heldObj.name);

            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * storeRange, Color.red,2f); // To test colider/rb info.
            if (Physics.Raycast(transform.position, transform.forward, out hit, storeRange))
            {
                Debug.Log("F raycast hit: " + hit.transform.gameObject.name);
                Debug.Log("F raycast tag: " + hit.transform.gameObject.tag);

                int gridX;
                int gridY;

                grid.GetGridCoords(hit.point, out gridX, out gridY);
                Debug.Log("World position: " + hit.point); //Tells the position where the coordinate has hit in the world
                Debug.Log("Gird coords: " + gridX + " " + gridY); //Tells the position of which grid-coordinate does that position corespond to.


                if (hit.transform.gameObject.tag == "canStore") //Verifies if the object is being placed in a "storable" area (BOTH REQUIRED)
                //&& hit.transform.gameObject.layer == LayerMask.NameToLayer("storeLayer"), trying without tag. Future DVD here, tag removed for redundancy.
                {
                    //StoreInShelf(hit.transform.gameObject); Previous system: currently trying to overhaul with grid system.
                    StoreInShelf(hit.transform.gameObject, gridX, gridY);
                }
            }
            else
            {
                Debug.Log("F raycast hit nothing");
            }
        }
    }

    void StoreInShelf(GameObject StoreObj, int gridX, int gridY)
    {
        GameObject heldObj = pickUpScript.GetHeldObject();
        Rigidbody heldObjRb = pickUpScript.GetHeldObjectRigidbody();

        if (heldObj == null || heldObjRb == null)
        {
            return;
        }

        inShelf_ = true;
        shelfObj = StoreObj;

        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(),shelfObj.GetComponent<Collider>(),true); //Setting to true for debugging

        heldObj.layer = LayerNumStore;

        //disable physics on the item in shelf
        heldObjRb.isKinematic = true;
        heldObjRb.linearVelocity = Vector3.zero;
        heldObjRb.angularVelocity = Vector3.zero;

        //remove item from player's hand
        //Testing, seeing if this fixes the weird angles/inconsistencies while placing an item.
        pickUpScript.ClearHeldObject();

        //place the item in the shelf
        //heldObj.transform.parent = shelfObj.transform; According to the Unity discord server, this is the root cause of placed-item's being deformed in unexpected angles
        heldObj.transform.SetParent(null);

        Vector3 gridWorldPosition = grid.GetWorldPositionCenter(gridX, gridY); //gridX and gridY should (in theory) be global.
        /*
        heldObj.transform.position = gridWorldPosition;
        heldObj.transform.rotation = Quaternion.identity;
        */
        heldObj.transform.SetPositionAndRotation(gridWorldPosition, Quaternion.identity); //Someone on Unity discord said this would be better with less inconsistencies, because both operation would happen simultaneously instead of line-by-line

        //heldObj.transform.localPosition = Vector3.zero;  //might have to replace with global if we want more uniformity in the placement of the wands
        //Function is more efficient + better
        //Replaces setting position and transfrom seperately
        //heldObj.transform.SetLocalPositionAndRotation( Vector3.zero, Quaternion.identity); I think this is causing the problem in the new system
    }
}