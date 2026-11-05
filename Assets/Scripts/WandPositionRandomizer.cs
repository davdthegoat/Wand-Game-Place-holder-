//Trying to make it so that this works in editor, and not in play mode, as that would significantly reduce lag probably.
using UnityEngine;

public class WandPositionRandomizer : MonoBehaviour
{
    [SerializeField] Vector3 positionRange = new Vector3(50, 10, 50); //Clamp of where wands can be spawned. Radial in nature

    [ContextMenu("Randomize Wand Positions")]

    void RandomizePositions()
    {
        foreach (Transform wand in transform)
        {
            //Change positions of wands
            wand.localPosition = new Vector3(Random.Range(-positionRange.x, positionRange.x)
                                            ,Random.Range(-positionRange.y, positionRange.y)
                                            ,Random.Range(-positionRange.z, positionRange.z));

            //Change scale of wands
            int randomScale = Random.Range(10, 19);
            float scale = randomScale / 10f;
            wand.localScale *= scale;
        }
    }
}
