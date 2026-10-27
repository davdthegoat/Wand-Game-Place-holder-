using CodeMonkey.MonoBehaviours;
using UnityEngine;
//Found a good tutorial, going to drop the link in discord.

//Going back to basics - using an empty gameObject instead of adding shader to children gameObjects.
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
        currentOutlinedObject = target;
        Renderer targetRenderer = target.transform.GetChild(0).GetComponent<Renderer>(); //Shader outline drawer has to be first child.
        if (targetRenderer != null)
        {
            targetRenderer.enabled = true;
            targetRenderer.material = outlineMaterial;
        }
    }

    public void ClearOutline()
    {
        if (currentOutlinedObject == null)
        {
            return; //Nothing highlighted, nothing to do.
        }

        Renderer targetRenderer = currentOutlinedObject.transform.GetChild(0).GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetRenderer.enabled = false; //Disable the shader outliner gameObject.
        }

        currentOutlinedObject = null;
    }
}
