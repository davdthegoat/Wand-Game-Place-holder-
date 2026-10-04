using Unity.Properties;
using UnityEngine;

public class StoreInShelfScript : MonoBehaviour
{
    //Dont need player collision to be disabled when storing in shelf
    //[SerializeField] GameObject player;
    [SerializeField] float storeRange = 5f;
    //[SerializeField] private TestingGrid testinggrid; //Might have to think of a smarter solution. This is attacthed to main camera, would have to add atleast 20 of these otherwise.
    private int LayerNumStore;
    private GameObject shelfObj;
    //private bool inShelf_; Worked around using Gird script
    private PickUpScript pickUpScript;
    //private TestingGrid testingGrid;
    //private Grid grid; 

    //An attempt to search through multiple grids, using an Array in order to be able to identify which one was called.
    private TestingGrid[] testingGrids;

    private Inventory inventory;

    void Start()
    {
        LayerNumStore = LayerMask.NameToLayer("storeLayer");
        pickUpScript = GetComponent<PickUpScript>();

        testingGrids= FindObjectsByType<TestingGrid>(FindObjectsSortMode.None);

        //Debug.Log("StoreInShelfScript has found pickUpScript " + (pickUpScript != null)); No longer useful, code works.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject heldObj = pickUpScript.GetHeldObject();
            Rigidbody heldObjRb = pickUpScript.GetHeldObjectRigidbody();
            /*
            Debug.Log("F was pressed");
            Debug.Log("PickUpScript reference " + pickUpScript); 
            Debug.Log("heldObj immediately after pickup: " + heldObj);
            Debug.Log("heldObjRb immediately after pickup: " + heldObjRb);
            */

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
                //Debug.Log("F raycast hit: " + hit.transform.gameObject.name);
                //Debug.Log("F raycast tag: " + hit.transform.gameObject.tag);
                TestingGrid hitTestingGrid = null;

                foreach (TestingGrid testingGrid in testingGrids)
                { 
                    if (testingGrid.WasHitByRay(hit))
                    {
                        hitTestingGrid = testingGrid;

                        inventory = hitTestingGrid.ReturnInventory();
                        
                        
                        break; //Exits for loop if the correct grid is found by cycling through the TestingGrid array
                    }
                }
                
                if (hitTestingGrid == null)
                {
                    Debug.Log("Raycast hit something which doesn't belong to testing grid");
                    return;
                }
                Grid grid = hitTestingGrid.GetGrid();
                int gridX;
                int gridY;

                grid.GetGridCoords(hit.point, out gridX, out gridY);
                Debug.Log("World position: " + hit.point); //Tells the position where the coordinate has hit in the world
                Debug.Log("Gird coords: " + gridX + " " + gridY); //Tells the position of which grid-coordinate does that position corespond to.
                //Further debugging for ZY axis.
                Debug.Log("Hit Object: " + hit.transform.gameObject.name);
                Debug.Log("Tag: " + hit.transform.gameObject.tag + "Layer: " + LayerMask.NameToLayer("storeLayer"));


                if (hit.transform.gameObject.tag == "canStore" && hit.transform.gameObject.layer == LayerMask.NameToLayer("storeLayer")) //Verifies if the object is being placed in a "storable" area (BOTH REQUIRED)
                //&& hit.transform.gameObject.layer == LayerMask.NameToLayer("storeLayer"), trying without tag. Future DVD here, tag removed for redundancy.
                {
                    int lowestFreeY = grid.GetLowestFreeGrid(gridX);
                    if (lowestFreeY != -1)
                    { 
                        StoreInShelf(hit.transform.gameObject, gridX, lowestFreeY, hitTestingGrid, hit.point);
                        grid.SetGridValue(gridX, lowestFreeY, 1);
                    }
                    /* Code working, no need for this debug.
                    else
                    {
                        Debug.Log("That grid is occupied by an object");
                    }
                    */
                }
            }
            else
            {
                Debug.Log("F raycast hit nothing");
            }
        }
    }

    void StoreInShelf(GameObject StoreObj, int gridX, int gridY, TestingGrid hitTestingGrid, Vector3 hitPoint)
    {
        GameObject heldObj = pickUpScript.GetHeldObject();
        Rigidbody heldObjRb = pickUpScript.GetHeldObjectRigidbody();
        
        //INVENTORY
        var droppedItem = heldObj.GetComponent<DroppedItem>();
        inventory.AddItem(droppedItem.item);
        //INVENTORY

        if (heldObj == null || heldObjRb == null)
        {
            return;
        }

        
        shelfObj = StoreObj;

        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(),shelfObj.GetComponent<Collider>(),false); //Setting to true for debugging

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

        Grid grid = hitTestingGrid.GetGrid();
        Vector3 gridWorldPosition = grid.GetWorldPositionCenter(gridX, gridY);
        Quaternion itemRotation = hitTestingGrid.GetItemRotation();


        StoredGridItem storedGridItem = heldObj.GetComponent<StoredGridItem>();
        if (storedGridItem == null)
        {
            Debug.Log("Item doesn't have a StoredGridItem" + heldObj.name);
            return;
            //storedGridItem = heldObj.AddComponent<StoredGridItem>();
        }

        //Determines which face is closest to the raycast hitpoint of grid
        float distanceToFaceA = Vector3.Distance(hitPoint, storedGridItem.faceA.position);
        float distanceToFaceB = Vector3.Distance(hitPoint, storedGridItem.faceB.position);

        if (distanceToFaceA <= distanceToFaceB)
        {
            storedGridItem.anchorFace = StoredGridItem.AnchorFace.FaceA;
        }
        else
        {
            storedGridItem.anchorFace = StoredGridItem.AnchorFace.FaceB;
        }


        //Determines which face the item should be anchored at to the grid.
        heldObj.transform.rotation = itemRotation;
        if (storedGridItem.anchorFace == StoredGridItem.AnchorFace.FaceB)
        {
            heldObj.transform.Rotate(0, 180f, 0);
        }

        Transform anchor;
        if (storedGridItem.anchorFace == StoredGridItem.AnchorFace.FaceA)
        {
            anchor = storedGridItem.faceA;
        }
        else
        {
            anchor = storedGridItem.faceB;
        }
        //Attempting to debug what is the root cause of the null exception.
        Debug.Log("Stored Grid Item: " + storedGridItem);
        Debug.Log("Anchor: " + anchor);
        Debug.Log("FaceA: " + storedGridItem.faceA);
        Debug.Log("FaceB: " + storedGridItem.faceB);
        Vector3 anchorWorldOffset = anchor.position - heldObj.transform.position;
        heldObj.transform.position = gridWorldPosition - anchorWorldOffset;
        /*
        heldObj.transform.position = gridWorldPosition;
        heldObj.transform.rotation = Quaternion.identity;
        */
        //heldObj.transform.SetPositionAndRotation(gridWorldPosition, Quaternion.identity); //Someone on Unity discord said this would be better with less inconsistencies, because both operation would happen simultaneously instead of line-by-line

        //heldObj.transform.localPosition = Vector3.zero;  //might have to replace with global if we want more uniformity in the placement of the wands
        //Function is more efficient + better
        //Replaces setting position and transfrom seperately
        //heldObj.transform.SetLocalPositionAndRotation( Vector3.zero, Quaternion.identity); I think this is causing the problem in the new system

        //Stores where on the grid the item is stored, can be retrieved/overwritten when item is taken out
        storedGridItem.testingGrid = hitTestingGrid;
        storedGridItem.gridX = gridX;
        storedGridItem.gridY = gridY;
    }
}