//Script will be attached to the ItemPlacementVolume gameObject, meaning every selectable and duplicatable WandBox will have this script.
//Copied the essence from a tutorial. This is beyond me

//Merging WorkStationWandBoxColliderManager into this script.
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkStationSnap : MonoBehaviour
{
    //[SerializeField] WorkStationWandBoxColliderManager wandBoxColliderManager; //To be used to determine collider behaviour for duplicated wandboxes.

    [Header("Item snapping")] //Discovered this neat organization trick for inspector.
    [SerializeField] private BoxCollider itemPlacementVolume;
    [SerializeField] private Transform wandBox;
    private readonly List<GameObject> placedItems = new List<GameObject>();

    
    [Header("Box colliders")]
    [SerializeField] private Collider wandBoxCollider;
    [SerializeField] private Collider wandBoxLidCollider;
    [SerializeField] private Collider wandBoxContainerCollider;
    

    private void Awake()
    {
        if (itemPlacementVolume == null)
        {
            itemPlacementVolume = GetComponent<BoxCollider>(); //Unsure if I continue with regular or box collider
        }
    }

    public bool TrySnap(GameObject item) //Changes from Unity discord server help.
    {
        if (item == null || itemPlacementVolume == null) //If no item
            return false;

        Debug.Log("TrySnap called for item: " + item.name); //Moved here to avoid null references.


        if (item.name == "WandboxLid") //Don't want lid to go into wandbox. It'll go on top, to close. Will make a function for that later.
            return false;

        if (item.GetComponentInChildren<Collider>() == null)
            return false;

        Bounds itemBounds;

        if (!TryGetItemBounds(item, out itemBounds))
            return false;

        Bounds volumeBounds = itemPlacementVolume.bounds;

        //Ignore the box if item is released outside box
        if (!volumeBounds.Intersects(itemBounds))
        {
            return false;
        }

        Vector3 size = itemBounds.size;

        //If the item can't fit inside the box.
        if (size.x > volumeBounds.size.x || size.y > volumeBounds.size.y || size.z > volumeBounds.size.z) //Won't actually need to use all 3. But Idk the exact orientation of the wand box so this is what I'll use for now.
        {
            Debug.Log("Rejected item " + item.name + ", it's dimensions are too large");
            return false;
        }
        //Transform item to middle of wandbox
        Vector3 wandboxCenter = wandBox.position;
        Vector3 movement = wandboxCenter - itemBounds.center;
        item.transform.position += movement;

        Rigidbody rb = item.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        if (!placedItems.Contains(item))
        {
            placedItems.Add(item);
        }

        Debug.Log("Success, item snapped");

        return true;
    }

    private bool TryGetItemBounds(GameObject item, out Bounds bounds)
    {
        Collider[] colliders = item.GetComponentsInChildren<Collider>();

        bounds = new Bounds();
        bool foundCollider = false;

        foreach (Collider col in colliders)
        {
            if (col.isTrigger || !col.enabled)
            {
                continue;
            }

            if (!foundCollider)
            {
                bounds = col.bounds;
                foundCollider = true;
            }
            else
            {
                bounds.Encapsulate(col.bounds);
            }
        }

        return foundCollider;
    }

    
    public void SetBoxOpen() //Actually, maybe this should be in WandboxSelectionScript now that I think of it. //Future DVD, tried that. Didn't work. Back here we go.
    {
        wandBoxCollider.enabled = false;
        wandBoxLidCollider.enabled = true;
        wandBoxContainerCollider.enabled = true;
    }

    public void SetBoxClosed()
    {
        wandBoxCollider.enabled = true;
        wandBoxLidCollider.enabled = false;
        wandBoxContainerCollider.enabled = false;
    }
    
}
