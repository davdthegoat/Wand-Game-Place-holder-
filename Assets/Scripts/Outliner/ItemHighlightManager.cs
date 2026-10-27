using UnityEngine;

public class ItemHighlightManager : MonoBehaviour
{
    [SerializeField] Material outlineMaterial;

    GameObject currentOutlinedObject;
    Renderer currentOutlinedRenderer;
    Material[] originalMaterials; //Making this an array because what if an object has loads of materials.

    public void DrawOutline(GameObject target)
    {
        if (currentOutlinedObject == target)
        {
            return; //Already highlighted, nothing to do.
        }

        ClearOutline(); //If existed, make sure to remove. Might not be necessary

        if (target == null)
        {
            return; //Nothing to highlight, nothing to do.
        }

        Renderer targetRenderer = target.GetComponentInChildren<Renderer>();
        if (targetRenderer == null || outlineMaterial == null)
        {
            return; //To avoid null reference exception errors.
        }

        currentOutlinedObject = target;
        originalMaterials = targetRenderer.materials;

        Material[] outlinedMaterials = new Material[originalMaterials.Length + 1];

        for (int i =0; i< originalMaterials.Length; i++)
        {
            outlinedMaterials[i] = originalMaterials[i]; //Copies old materials and stores them here.
        }

        outlinedMaterials[outlinedMaterials.Length - 1] = outlineMaterial; //Sets outline material in inspector to last slot.

    }

    public void ClearOutline()
    {
        if (currentOutlinedObject == null)
        {
            return; //Nothing highlighted, nothing to do.
        }

        Renderer targetRenderer = currentOutlinedObject.GetComponentInChildren<Renderer>(); //Maybe not in children, idk how inspector setup will be like, but I think children.

        if (targetRenderer != null && originalMaterials != null)
        {
            targetRenderer.materials = originalMaterials; //Restore previous materials
        }

        currentOutlinedObject = null; //Clear references, prepare for next call of DrawOutline.
        originalMaterials = null;
    }
}
