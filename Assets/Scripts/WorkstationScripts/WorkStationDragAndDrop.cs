//THIS IS ATTACHED TO A GAMEOBJECT WITHIN TABLE. THIS IS NOT ON THE PLAYER, BUT A EMPTY GAMEOBJECT.
using Unity.VisualScripting;
using UnityEngine;

public class WorkStationDragAndDrop : MonoBehaviour
{
    [SerializeField] Camera sitPositionCamera;
    [SerializeField] WorkStationSit workStationSit; //To check when player is sitting or standing.
    [SerializeField] private float dragRange = 10f; //How close to the cursor the item's collider must be
    [SerializeField] private float dragStrength; //How strongly is the item pulled. Might not be used.
    [SerializeField] private Transform itemPlacementPosition; //Same one from HeldObjectPlacementScript, same gameObject too.
    [SerializeField] private Transform wandLidPlacementPosition; //Same one from WandBoxSelectionScript, using same gameObject.
    [SerializeField] private LayerMask layersToIgnore;
    [SerializeField] private HeldObjectPlacementScript heldObjectPlacementScript;
    private GameObject draggedObject;
    private Rigidbody draggedObjectRb;
    private float dragDepth; //if we want to freeze movement in one axis.
    private Vector3 dragOffset;
    private bool wasKinematic; //Might remove

    private Plane dragPlane;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private GameObject selectedWand;
    private GameObject selectedLid;
    private bool wasSitting;

    private bool wandInPlace = false;
    private bool boxPacked = false;

    //INVENTORY
    [HideInInspector]
    public GameObject currentWandinBox;


    void Update()
    {
        if (workStationSit.IsSitting() == false)
        {
            if (wasSitting)
            {
                StopDragging();

                if (!boxPacked)
                {
                    ReturnObjectToPosition(selectedWand, itemPlacementPosition);
                    if (selectedLid != null && selectedLid.name == "WandboxLid")
                    {
                        ReturnObjectToPosition(selectedLid, wandLidPlacementPosition);
                    }
                }

                selectedWand = null;
                selectedLid = null;
            }

            wasSitting = false;
            return;
        }
        wasSitting = true;

        if (Input.GetMouseButtonDown(0))
        { 
            TryDragging();
        }

        if (Input.GetMouseButton(0))
        {
            if (draggedObject != null)
            {
                DragObject();
            }
        }

        if (Input.GetMouseButtonUp(0)) //Additions to make this script play nice with WorkStationSnap script
        {
            //Refactoring for ease of readability and editing. I can't make sense of this in VS debugger.
            TryToSnap();
        }
    }


    void TryDragging()
    {
        Ray ray = sitPositionCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, dragRange, ~layersToIgnore))
        {
            if (hit.collider.CompareTag("WorkStationItem") || (/*--------*/hit.collider.CompareTag("canPickUp") && hit.transform.gameObject.layer == LayerMask.NameToLayer("WorkStationDrag")/*-------*/ )) //Makes it so that wandboxes are un-draggable.
            {

                draggedObject = hit.collider.gameObject;
                draggedObjectRb = draggedObject.GetComponent<Rigidbody>();

                if (draggedObject.name == "WandboxLid")
                {
                    selectedLid = draggedObject; //Assigns value of last-selected/most recently selected lid to variable selected lid.
                }
                else if (draggedObject.name != "WandboxLidClosed") //Elif if we add decals or something else here. Update will have to change too
                {
                    selectedWand = draggedObject; //INVENTORY //Possibly useful for you.
                }

                //To reset item's position when you stand back up.
                originalPosition = draggedObject.transform.position;
                originalRotation = draggedObject.transform.rotation;

                //Trying to make it so this plane isn't created every frame, for consistency in movement, performance and debugging. //Failed
                dragPlane = new Plane(sitPositionCamera.transform.forward, draggedObject.transform.position);

                if (dragPlane.Raycast(ray, out float distance))
                {
                    Vector3 mouseWorldPosition = ray.GetPoint(distance);
                    dragOffset = draggedObject.transform.position - mouseWorldPosition;

                    Debug.Log("Initial plane intersection succeeded. Distance: " + distance);
                }
                else
                {
                    Debug.Log("Initial plane intersection failed");
                }

                if (draggedObjectRb != null)
                {
                    wasKinematic = draggedObjectRb.isKinematic;
                    draggedObjectRb.isKinematic = true;
                }
                Debug.Log("Selected: " + draggedObject.name);
                Debug.Log("Plane selected.");
            }
        }
    }


    void DragObject()
        {
        Ray ray = sitPositionCamera.ScreenPointToRay(Input.mousePosition);
        //Plane dragPlane = new Plane(sitPositionCamera.transform.forward, draggedObject.transform.position);

        if (dragPlane.Raycast(ray, out float distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance) + dragOffset;
                //Debug.Log("Taget position: " + targetPosition);
                //Debug.Log("Current Position:" + draggedObject.transform.position);
            draggedObject.transform.position = targetPosition; //For debugging
                //Debug.Log("Position after movement:" + draggedObject.transform.position);
                //Commented out the debugs above because they were absolutely SPAMMING my console. Not testing this further anyways

            if (draggedObjectRb != null)
            {
                draggedObjectRb.isKinematic = true;
              draggedObjectRb.useGravity = false;
            }
        }
    }

        void StopDragging()
        {
            if (draggedObject == null) //Early exit, not all left clicks set "draggedObject".
            {
                return;
            }

            if (draggedObjectRb != null)
            {
                draggedObjectRb.isKinematic = wasKinematic;
            }

            Debug.Log("Stopped item dragging: " + draggedObject.name);

            //return to OG position
            //draggedObject.transform.position = originalPosition; //Doing in update.
            //draggedObject.transform.rotation = originalRotation;

            //Physics behavior
            draggedObjectRb.isKinematic = false;
            draggedObjectRb.useGravity = true;

            //clear assignments
            draggedObject = null;
            draggedObjectRb = null;
        }

        private void ReturnObjectToPosition(GameObject selectedObject, Transform targetLocation)
        {
            if (selectedObject == null || targetLocation == null)
            {
                return;
            }

            selectedObject.transform.position = targetLocation.position;
            selectedObject.transform.rotation = targetLocation.rotation;

            Rigidbody rb = selectedObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }

        private void TryToSnap()
        {
            GameObject releasedObject = draggedObject;

            StopDragging();

            if (releasedObject == null || !releasedObject.CompareTag("WorkStationItem") && !releasedObject.CompareTag("canPickUp"))
            {
                return;
            }

            if (releasedObject.name == "WandboxLid")
            {
                if (wandInPlace == false) //Use this value to transfer item data to box inventory -DVD.
                {
                    Debug.Log("Snapping rejected, wand not in place");
                    return;
                }

                WorkStationSnap[] snapSystems = FindObjectsByType<WorkStationSnap>(FindObjectsSortMode.None);
                foreach (WorkStationSnap snapSystem in snapSystems)
                {
                    Transform box = snapSystem.transform.parent;

                    if (box == null)
                    {
                        continue;
                    }

                    Transform lidVolumeTransform = box.Find("WandboxLidPlacementVolume");

                    if (lidVolumeTransform == null)
                    {
                        continue;
                    }

                    Collider lidVolume = lidVolumeTransform.GetComponent<Collider>();
                    Collider lidCollider = releasedObject.GetComponent<Collider>();

                    if (lidVolume == null || lidCollider == null)
                    {
                        continue;   
                    }
                    if (lidVolume.bounds.Intersects(lidCollider.bounds))
                    {
                        Debug.Log("Lid snapped successfully");
                        //Return lid to it's closed state.
                        Transform lidClosedPosition = box.Find("WandboxLidClosed");
                        releasedObject.transform.position = lidClosedPosition.transform.position;
                        releasedObject.transform.rotation = lidClosedPosition.transform.rotation;


                        //INVENTORY
                        currentWandinBox = WandinBox(selectedWand);


                        snapSystem.SetBoxClosed();

                        releasedObject.name = "WandboxLidClosed";

                        box.tag = "canPickUp";
                        box.gameObject.layer = LayerMask.NameToLayer("storeLayer");
                        releasedObject.tag = "Untagged";
                        
                        
                        WandBox wandboxref = box.GetComponent<WandBoxItem>().wandboxitem;
                        Wand wandDataref = selectedWand.GetComponent<DroppedItem>().item;
                        wandboxref.wandinBox = wandDataref;
                        
                        
                        //DELETE WAND
                        Destroy(selectedWand);
                        
                        boxPacked = true; //Use this for Inventory/destroying stored item. //INVENTORY
                        
                        

                        Rigidbody lidRb = releasedObject.GetComponent<Rigidbody>();
                        if (lidRb != null)
                        {
                            Debug.Log("Item should no longer fall through world");
                            lidRb.angularVelocity = Vector3.zero;
                            lidRb.linearVelocity = Vector3.zero;
                            lidRb.useGravity = false;
                            lidRb.isKinematic = true;
                        }
                        break;
                    }
                }
                return;
            }
            WorkStationSnap[] systems = FindObjectsByType<WorkStationSnap>(FindObjectsSortMode.None);

            foreach (WorkStationSnap snapSystem in systems)
            {
                if (snapSystem.TrySnap(releasedObject))
                {
                    wandInPlace = true;
                    if (releasedObject.name != "WandboxLid")
                    {
                        heldObjectPlacementScript?.ClearItemOnTable(releasedObject);
                    }

                    break;
                }
            }
        }
    public void ResetPackingState()
    {
        wandInPlace = false;
        boxPacked = false;
    }
        



    //Idk why this has weird indentation
    //INVETORY ASSISTANCE 
    //Trying to help with inventory work.
    public bool IsBoxFullyClosed()
    {
        return boxPacked;
    }
    //Unless I'm wrong you just need the name in string of the gameObject being stored right? This should do it.
    
    public GameObject WandinBox(GameObject selectedWand)
    {
        
        return selectedWand;
         
        
        
    }

        
}

