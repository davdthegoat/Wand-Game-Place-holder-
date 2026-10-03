using UnityEngine;

public class StoredGridItem : MonoBehaviour
{
    //This script will store where exactly the item is stored on the grid in order to make retrieving and replacing items on the grid easier.
    public TestingGrid testingGrid;
    public int gridX;
    public int gridY;

    public Transform faceA;
    public Transform faceB;

    public enum AnchorFace
    {
        FaceA,
        FaceB
    }
    public AnchorFace anchorFace;
}
