using UnityEngine;
using CodeMonkey.Utils; //UN-COMMENT TO RESTORE GRIDE DEBUG FUNCTIONALITY

public class TestingGrid : MonoBehaviour
{
    //This is all in an attempt to make the code more readable and easier to work on through Unity inspector.
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float gridSize;
    [SerializeField] float xPos;
    [SerializeField] float yPos;
    [SerializeField] float zPos;
    [SerializeField] Grid.GridPlane gridPlane;
    [SerializeField] private Collider gridCollider; //Will help to uniquely identify a grid.
    [SerializeField] private Vector3 itemRotation;
    

    private Grid grid;

    void Awake() //So that grid is initialized before StoreInShelfScript everytime.
    {
        //Use this to change size, shape, position of grid on the world
        grid = new Grid(width, height, gridSize, transform.position, gridPlane, transform.rotation); //the third parameter here is cellSize, change it to change how many units apart each grid should be.
        //new Vector3 decides position of the grid, in GLOBAL coords.
    }
    public Grid GetGrid()
    {
        return grid;
    }

    public Quaternion GetItemRotation()
    {
        Quaternion planeRotation = Quaternion.identity;
        if (gridPlane == Grid.GridPlane.ZY)
        {
            return Quaternion.Euler(itemRotation + new Vector3(0, 90f, 0));
        }
        return transform.rotation * planeRotation * Quaternion.Euler(itemRotation);
    }

    private void Update()
    {
        //UNCOMMENT CODE BELOW TO REENABLE DEBUGGING FOR GRID COORDINATE SYSTEM
        /*if (Input.GetMouseButtonDown(0)) //If left click is pressed while mouse is over grid, change value of grid using "SetGridValue" method defined in Grid script.
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);
            grid.SetGridValue(UtilsClass.GetMouseWorldPosition(), 1);


        }
        */

        if (Input.GetMouseButtonDown(1)) //Button to get the value stored inside a grid
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        } 
    }

    public bool WasHitByRay(RaycastHit hit)
    {
        return hit.collider == gridCollider;
    }

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        for (int x  =0; x <= width; x++)
        {
            Vector3 start;
            Vector3 end;

            if (gridPlane == Grid.GridPlane.XY)
            {
                start = new Vector3(x * gridSize, 0, 0);
                end = new Vector3(x * gridSize, height * gridSize, 0);
            }
            else
            {
                start = new Vector3(0, 0, x * gridSize);
                end = new Vector3(0, height * gridSize, x * gridSize);
            }
            Gizmos.DrawLine(transform.TransformPoint(start), transform.TransformPoint(end));
        }

        for (int y =0; y <= height; y++)
        {
            Vector3 start;
            Vector3 end;

            if (gridPlane == Grid.GridPlane.XY)
            {
                start = new Vector3(0, y * gridSize, 0);
                end = new Vector3(width * gridSize, y * gridSize, 0);
            }
            else
            {
                start = new Vector3(0, y * gridSize, 0);
                end = new Vector3(0, y * gridSize, height * gridSize);
            }
            Gizmos.DrawLine(transform.TransformPoint(start), transform.TransformPoint(end));
        }
    }
}