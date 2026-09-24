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
    }

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

            if (Physics.Raycast(
                transform.position,
                transform.TransformDirection(Vector3.forward),
                out hit,
                pickUpRange))
            {
                if (hit.transform.gameObject.tag == "canStore")
                {
                    StoreInShelf(hit.transform.gameObject);
                }
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
        heldObj.transform.localPosition = Vector3.zero;
        heldObj.transform.localRotation = Quaternion.identity;

        //remove item from player's hand
        pickUpScript.ClearHeldObject();
    }
}