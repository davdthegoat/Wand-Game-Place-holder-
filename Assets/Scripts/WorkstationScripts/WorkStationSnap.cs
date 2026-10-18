//Script will be attached to the ItemPlacementVolume gameObject, meaning every selectable and duplicatable WandBox will have this script.
//Copied the essence from a tutorial. This is beyond me
using System.Collections.Generic;
using UnityEngine;

public class WorkStationSnap : MonoBehaviour
{
    [SerializeField] WorkStationWandBoxColliderManager wandBoxColliderManager; //To be used to determine collider behaviour for duplicated wandboxes.


    [SerializeField] private BoxCollider itemPlacementVolume;
    [SerializeField] private float spacing = 0.02f;
    private readonly List<GameObject> placedItems = new List<GameObject>();

    private void Awake()
    {
        if (itemPlacementVolume == null)
        {
            itemPlacementVolume = GetComponent<BoxCollider>(); //Unsure if I continue with regular or box collider
        }
    }

    public bool TrySnap(GameObject item)
    {
        if (item == null || itemPlacementVolume == null) //If no item
            return false;

        Debug.Log("TrySnap called for item: " + item.name); //Moved here to avoid null references.


        if (item.name == "WandboxLid")
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
            return false;
        }

        float stepX = size.x + spacing;
        float stepY = size.y + spacing;
        float stepZ = size.z + spacing;

        for (float y = volumeBounds.min.y + size.y / 2f; y <= volumeBounds.max.y - size.y / 2f; y += stepY)
        {
            for (float z = volumeBounds.min.z + size.z / 2f; z <= volumeBounds.max.z - size.z / 2f; z += stepZ)
            {
                for (float x = volumeBounds.min.x + size.x / 2f; x <= volumeBounds.max.x - size.x / 2f; x += stepX)
                {
                    Vector3 candidateCenter = new Vector3(x, y, z);

                    Bounds candidateBounds = new Bounds(candidateCenter, size);

                    if (OverlapsPlacedItems(candidateBounds))
                        continue;

                    Vector3 movement = candidateCenter - itemBounds.center;
                    item.transform.position += movement;

                    Rigidbody rb = item.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }

                    if (!placedItems.Contains(item))
                    {
                        placedItems.Add(item);
                    }

                    Debug.Log("Snapped " + item.name + " into " + name);
                    return true;
                }
            } 
        }

        Debug.Log("No suitable position found for " + item.name + ", container too small");
        return false;
    }

    private bool OverlapsPlacedItems(Bounds candidateBounds)
    {
        for (int i = placedItems.Count -1; i >= 0; i--)
        {
            GameObject placedItem = placedItems[i];

            if (placedItem == null)
            {
                placedItems.RemoveAt(i);
                continue;
            }

            Bounds placedBounds; 

            if (TryGetItemBounds(placedItem, out placedBounds))
            {
                if (candidateBounds.Intersects(placedBounds))
                {
                    return true;
                }
            }
        }

        return false;
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
}
