using UnityEngine;

public class StoreInShelfScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float pickUpRange = 5f;

    private int LayerNumStore;
    private GameObject shelfObj;
    private bool inShelf_;
    private PickUpScript pickUpScript;


    void Start()
    {
        LayerNumStore = LayerMask.NameToLayer("storeLayer");
        pickUpScript = GetComponent<PickUpScript>();

        Debug.Log("StoreInShelfScript has found pickUpScript " + (pickUpScript != null));
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

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
            {
                Debug.Log("F raycast hit: " + hit.transform.gameObject.name);
                Debug.Log("F raycast tag: " + hit.transform.gameObject.tag);

                if (hit.transform.gameObject.tag == "canStore" && hit.transform.gameObject.layer == LayerMask.NameToLayer("storeLayer"))
                {
                    StoreInShelf(hit.transform.gameObject);
                }
            }
            else
            {
                Debug.Log("F raycast hit nothing");
            }
        }
    }

    void StoreInShelf(GameObject StoreObj)
    {
        GameObject heldObj = pickUpScript.GetHeldObject();
        Rigidbody heldObjRb = pickUpScript.GetHeldObjectRigidbody();

        if (heldObj == null || heldObjRb == null)
        {
            return;
        }

        inShelf_ = true;
        shelfObj = StoreObj;

        Physics.IgnoreCollision(
            heldObj.GetComponent<Collider>(),
            player.GetComponent<Collider>(),
            false
        );

        heldObj.layer = LayerNumStore;

        //disable physics on the item in shelf
        heldObjRb.isKinematic = true;
        heldObjRb.linearVelocity = Vector3.zero;
        heldObjRb.angularVelocity = Vector3.zero;

        //p[ace the item in the shelf
        heldObj.transform.parent = shelfObj.transform;
        heldObj.transform.localPosition = Vector3.zero;  //might have to replace with global if we want more uniformity in the placement of the wands
        heldObj.transform.localRotation = Quaternion.identity;

        //remove item from player's hand
        pickUpScript.ClearHeldObject();
    }
}