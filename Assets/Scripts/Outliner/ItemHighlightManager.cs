using UnityEngine;
//Found a good tutorial, going to drop the link in discord.

//Going back to basics - using an empty gameObject instead of adding shader to children gameObjects.
public class ItemHighlightManager : MonoBehaviour
{
    [SerializeField] Material outlineMaterial;
    [SerializeField] Material outlineMaterialForBoxes;

    GameObject currentOutlinedObject;
    Renderer currentOutlinedRenderer;
    Renderer currentOutlinedChildRenderer;
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
        currentOutlinedObject = target;
        Renderer targetRenderer = target.GetComponent<Renderer>();

        if (targetRenderer == null) //When item doesn't have it's own mesh renderer.    
        {
            Renderer targetRendererOfChild = target.transform.GetChild(0).GetComponent<Renderer>(); //If object doesn't have mesh renderer, get from (first) child.
            targetRendererOfChild.material = outlineMaterialForBoxes; //This is a bandaid solution for a cheaply-made, low effort outline shader. I just made a duplicate, this one has slightly different values, more suited for the cuboid shaped wandboxes.
            targetRendererOfChild.enabled = true;
            currentOutlinedChildRenderer = targetRendererOfChild;
        }
        if (targetRenderer != null) 
        {
            currentOutlinedRenderer = targetRenderer;
            originalMaterials = targetRenderer.materials; //Using this to restore any previous materials on the object which doesn't have children
            Material[] outlinedMaterials = new Material[originalMaterials.Length + 1];

            for (int i = 0; i < originalMaterials.Length; i++)
            {
                outlinedMaterials[i] = originalMaterials[i];
            }

            outlinedMaterials[outlinedMaterials.Length - 1] = outlineMaterial;

            targetRenderer.materials = outlinedMaterials;
        }
    }

    public void ClearOutline()
    {
        if (currentOutlinedObject == null)
        {
            return; //Nothing highlighted, nothing to do.
        }

        if (currentOutlinedObject != null && originalMaterials != null)
        {
            currentOutlinedRenderer.materials = originalMaterials;
        }
        else if (currentOutlinedChildRenderer != null)
        {
            currentOutlinedChildRenderer.enabled = false;

            //Clearing reference.
            currentOutlinedChildRenderer = null;
        }

        //Clearing references so next time code is called it works.
        currentOutlinedObject = null;
        currentOutlinedRenderer = null;
        originalMaterials = null;
    }
}
