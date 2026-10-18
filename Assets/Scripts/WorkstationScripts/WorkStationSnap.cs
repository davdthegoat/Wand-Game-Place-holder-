//Script will be attached to the ItemPlacementVolume gameObject, meaning every selectable and duplicatable WandBox will have this script.
//Copied the essence from a tutorial. This is beyond me
using System.Collections.Generic;
using UnityEngine;

public class WorkStationSnap : MonoBehaviour
{
    /*
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

        if (item.name == "WandboxLid")
            return false;

        if (item.GetComponentInChildren<Collider>() == null)
            return false;

        Bounds itemsBounds;

        if (!TryGetItemBounds(item, out itemsBounds))
            return false;

        Bounds volumeBounds = itemPlacementVolume.bounds;

        Vector3 size = itemBounds.size;

        //If the item can't fit inside the box.
        if (size.x > volumeBounds.size.x || size.y > volumeBounds.size.y || size.z > volumeBounds.size.z) //Won't actually need to use all 3. But Idk the exact orientation of the wand box so this is what I'll use for now.
        {
            return false;
        }

        float stepX = size.x + spacing;
        float stepY = size.y + spacing;
        float stepZ = size.z + spacing;

    }
    */
}
