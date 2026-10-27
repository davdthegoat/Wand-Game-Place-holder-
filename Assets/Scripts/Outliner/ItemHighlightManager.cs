using UnityEngine;

public class ItemHighlightManager : MonoBehaviour
{
    [SerializeField] Material outlineMaterial;

    GameObject currentOutlinedObject;
    Material[] originalMaterials; //Making this an array because what if an object has loads of materials.

    public void DrawOutline(GameObject target)
    {
        if (currentOutlinedObject == target)
        {
            return; //Already highlighted, nothing to do.
        }

        EraseOutline(); //If existed, make sure to remove. Might not be necessary

        if (target == null)
        {
            return; //Nothing to highlight, nothing to do.
        }

        Renderer targetRenderer = target.GetComponent<Renderer>();
        if (targetRenderer == null)
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
    }
}
