using CodeMonkey.MonoBehaviours;
using UnityEngine;
//Found a good tutorial, going to drop the link in discord.
public class ItemHighlightManager : MonoBehaviour
{
    [SerializeField] Material outlineMaterial;

    GameObject currentOutlinedObject;
    Renderer[] currentOutlinedRenderer;
    Material[][] originalMaterials; //Making this an array because what if an object has loads of materials.

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

        Renderer[] targetRenderer = target.GetComponentsInChildren<Renderer>(true);
        if (targetRenderer.Length == 0)
        {
            return; //To avoid null reference exception errors.
        }

        currentOutlinedObject = target;
        currentOutlinedRenderer = targetRenderer;
        originalMaterials = new Material[targetRenderer.Length][];
        

        for (int i =0; i< originalMaterials.Length; i++)
        {
            originalMaterials[i] = targetRenderer[i].materials;

            Material[] outlinedMaterials = new Material[originalMaterials[i].Length + 1];

            for (int j = 0; j < originalMaterials[i].Length; j++)
            {
                outlinedMaterials[j] = originalMaterials[i][j];
            }

            outlinedMaterials[outlinedMaterials.Length -1] = outlineMaterial; //Copies old materials and stores them here.

            targetRenderer[i].materials = outlinedMaterials;
        }
    }

    public void ClearOutline()
    {
        if (currentOutlinedObject == null)
        {
            return; //Nothing highlighted, nothing to do.
        }

        if (currentOutlinedRenderer != null && originalMaterials != null)
        {
            for (int i = 0; i < currentOutlinedRenderer.Length; i++)
            {
                if (currentOutlinedRenderer[i] != null)
                {
                    currentOutlinedRenderer[i].materials = originalMaterials[i];
                }
            }
        }

        currentOutlinedObject = null; //Clear references, prepare for next call of DrawOutline.
        originalMaterials = null;
        currentOutlinedRenderer = null;
    }
}
