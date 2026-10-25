using UnityEngine;
using UnityEngine.Assertions.Must;

public class WandboxSelectionScript : MonoBehaviour
{
    [SerializeField] WorkStationDragAndDrop workStationDragAndDrop;
    [SerializeField] private Camera playerCamera;
    //[SerializeField] private GameObject wandboxPrefab; //Don't think this is useful any longer. 
    [SerializeField] private Transform wandSpawnLocation;
    [SerializeField] private Transform wandLidSpawnLocation;
    [SerializeField] private float raycastRange = 100f;
    [SerializeField] private WorkStationSit workStationSit;

    /* Redundant.
    [Header("Wand Box Colliders")]
    [SerializeField] private Collider wandBoxCollider;
    [SerializeField] private Collider wandBoxLidCollider;
    [SerializeField] private Collider wandBoxContainerCollider;
    */
    private int count = 0;
    GameObject duplicatedBox;
    Transform lid;

    // Update is called once per frame
    void Update()
    {
        if (workStationSit.IsSitting() == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectWandBox();
            }
        }
    }

    private void SelectWandBox() //Chose the correct sized box for your needs.
    //Reused code from previous scripts mostly.
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (workStationSit.IsSitting() == true)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, raycastRange))
            {
                Debug.Log("Ray hit: " + hit.collider.name);
                if (hit.collider.CompareTag("WandBox"))
                {
                    if (count == 0) //First box spawn
                    {
                        duplicatedBox = Instantiate(hit.collider.gameObject, wandSpawnLocation.position, wandSpawnLocation.rotation); //Click on the appropriate sized box -> duplicate box spawns infron of you.

                        //Reseting packing state to avoid invalid box and thus wand deletions
                        workStationDragAndDrop.ResetPackingState();


                        WorkStationSnap duplicate = duplicatedBox.GetComponentInChildren<WorkStationSnap>();
                        duplicate.SetBoxOpen();
                        //duplicatedBox.tag = "canPickUp"; //So that wandbox can be picked up after item has been stored inside.
                        //Made the original item kinematic, removed need for next line.
                        //duplicatedBox.GetComponent<Rigidbody>().isKinematic = true;


                        //Creating and duplicating lid
                        lid = duplicatedBox.transform.Find("WandboxLid");

                        lid.position = wandLidSpawnLocation.position;
                        lid.rotation = wandLidSpawnLocation.rotation;
                        lid.tag = "WorkStationItem";
                        count += 1;
                    }
                    else if (count == 1)
                    {
                        if (workStationDragAndDrop.IsBoxFullyClosed() == true)
                        {
                            count = 0;
                            duplicatedBox = null; //Prevents destruction of a closed wandbox (which contains a wand).
                            return;
                        }
                        Destroy(duplicatedBox);

                        duplicatedBox = Instantiate(hit.collider.gameObject, wandSpawnLocation.position, wandSpawnLocation.rotation); //Destroys previously summoned box and replaces it with the newly clicked box-type.

                        

                        //Reseting packing state to avoid invalid box and thus wand deletions
                        workStationDragAndDrop.ResetPackingState();

                        WorkStationSnap duplicate = duplicatedBox.GetComponentInChildren<WorkStationSnap>();
                        duplicate.SetBoxOpen();

                        lid = duplicatedBox.transform.Find("WandboxLid");

                        lid.position = wandLidSpawnLocation.position;
                        lid.rotation = wandLidSpawnLocation.rotation;

                        lid.tag = "WorkStationItem"; //To enable item dragging FOR LID ONLY
                        //duplicatedBox.tag = "canPickUp";
                        //duplicatedBox.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
        }
    }
    /*Future DVD here. After writing all the code, and going to the inspector I realized why this code can't be here. It's not applied to the box, but to Mr. Bean. Meaning each and every boxs colliders would more or less have to either be identical, or individually assigned. Putting this back in WorkStationSnap script.
    private void SetBoxClosed() //Perhaps the opening of wand boxes should be here, but I don't think closing should.
    {
        wandBoxCollider.enabled = true;
        wandBoxLidCollider.enabled = false;
        wandBoxContainerCollider.enabled = false;
    }

    private void SetBoxOpen()
    {
        wandBoxCollider.enabled = false;
        wandBoxLidCollider.enabled = true;
        wandBoxContainerCollider.enabled = true;
    }
    */
}
