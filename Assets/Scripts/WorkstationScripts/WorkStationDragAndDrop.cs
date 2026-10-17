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
    

void Update()
    {
        if (workStationSit.IsSitting() == false)
        {
            if (wasSitting)
            {
                StopDragging();

                ReturnObjectToPosition(selectedWand, itemPlacementPosition);
                ReturnObjectToPosition(selectedLid, wandLidPlacementPosition);

                selectedWand = null;
                selectedLid = null;
            }

            wasSitting = false;
            return;
        }
        wasSitting = true;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse DOWN");
            TryDragging();
        }

        if (Input.GetMouseButton(0))
        {
            if (draggedObject != null)
            {
                DragObject();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse UP");
            StopDragging();
        }

        /*
        if (Input.GetKeyDown(KeyCode.F) && selectedObject != null)
        {
            if (selectedObject.CompareTag("WorkStationItem"))
            {
                //Determines where the item will be respawned when played exits workstation.
                Transform returnPosition = null;
                if (selectedObject.name == "WandboxLid")
                {
                    returnPosition = wandLidPlacementPosition;
                }
                else if (selectedObject.CompareTag("WorkStationItem"))
                {
                    returnPosition = itemPlacementPosition;
                }
                if (returnPosition != null)
                {
                    selectedObject.transform.position = returnPosition.position;
                    selectedObject.transform.rotation = returnPosition.rotation;
                }


                Rigidbody rb = selectedObject.GetComponent<Rigidbody>();
                if (rb != null) //Ensure that rigidbody is return to normal when played exits. While have to nest this in another loop depending on whether item is packaged or not maybe.
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }

                selectedObject = null;
            }
        }
        */
    }


    void TryDragging()
    {
        Ray ray = sitPositionCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, dragRange, ~layersToIgnore))
        {
            if (hit.collider.CompareTag("WorkStationItem")) //Makes it so that wandboxes are un-draggable.
            {

                draggedObject = hit.collider.gameObject;
                draggedObjectRb = draggedObject.GetComponent<Rigidbody>();

                if (draggedObject.name == "WandboxLid")
                {
                    selectedLid = draggedObject; //Assigns value of last-selected/most recently selected lid to variable selected lid.
                }
                else //Elif if we add decals or something else here. Update will have to change too
                {
                    selectedWand = draggedObject;
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

                    Debug.Log("Dragging start: " + draggedObject.name);
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
            Debug.Log("Taget position: " + targetPosition);
            Debug.Log("Current Position:" + draggedObject.transform.position);
            draggedObject.transform.position = targetPosition; //For debugging
            Debug.Log("Position after movement:" + draggedObject.transform.position);


            //For gravity (testing)
            draggedObjectRb.isKinematic = true;
            draggedObjectRb.useGravity = false;
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
}

