using Unity.VisualScripting;
using UnityEngine;

public class WorkStationDragAndDrop : MonoBehaviour
{
    [SerializeField] Camera sitPositionCamera;
    [SerializeField] WorkStationSit workStationSit; //To check when player is sitting or standing.
    [SerializeField] private float dragRange = 10f; //How close to the cursor the item's collider must be
    [SerializeField] private float dragStrength; //How strongly is the item pulled. Might not be used.

    private GameObject draggedObject;
    private Rigidbody draggedObjectRb;
    private float dragDepth; //if we want to freeze movement in one axis.
    private Vector3 dragOffset;
    private bool wasKinematic; //Might remove

    private Plane dragPlane;
    //dragPlane = new Plane(sitPositionCamera.transform.forward, draggedObject.transform.position);
    

    /*void Update()
    {
        if (workStationSit.IsSitting() == false) //Trying to reduce lag by nesting all of this inside a if statement.
        {
            StopDragging();
            return;
        }

        //Select object on first M1
        if (Input.GetMouseButtonDown(0))
        {
            TryDragging();
            Debug.Log("Try Dragging called");
        }
        //Drag item when M1 held, after initial press.
        if (draggedObject != null && Input.GetMouseButton(0))
        {
            DragObject();
        }
        //Stop drag when M1 lifted.
        if (draggedObject != null && Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    } */

void Update()
    {
        if (workStationSit.IsSitting() == false)
        {
            StopDragging();
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse DOWN");
            TryDragging();
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse HELD | Object: " +
                (draggedObject != null ? draggedObject.name : "NULL"));

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
    }


    void TryDragging()
    {
        Ray ray = sitPositionCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit hit, dragRange))
        {
            if (hit.collider.CompareTag("canPickUp"))
            {

                draggedObject = hit.collider.gameObject;
                draggedObjectRb = draggedObject.GetComponent<Rigidbody>();

                //Trying to make it so this plane isn't created every frame, for consistency in movement, performance and debugging.
                Plane dragPlane = new Plane(sitPositionCamera.transform.forward, draggedObject.transform.position);

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
            else
            {
                Debug.Log("Selection ray hit nothing");
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
        }
    }

    void StopDragging()
    {
        if (draggedObject == null) 
        {
            return;
        }

        if (draggedObjectRb != null)
        {
            draggedObjectRb.isKinematic = wasKinematic;
        }

        Debug.Log("Stopped item dragging: " + draggedObject.name);

        //clear assignments
        draggedObject = null;
        draggedObjectRb = null;
    }
}

