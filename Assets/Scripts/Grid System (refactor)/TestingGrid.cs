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
    //INVENTORY
    public Inventory inventory;
    //INVENTORY

    private Grid grid;

    void Awake() //So that grid is initialized before StoreInShelfScript everytime.
    {
        //Use this to change size, shape, position of grid on the world
        grid = new Grid(width, height, gridSize,new Vector3(xPos,yPos,zPos), gridPlane); //the third parameter here is cellSize, change it to change how many units apart each grid should be.
        //new Vector3 decides position of the grid, in GLOBAL coords.
    }
    public Grid GetGrid()
    {
        return grid;
    }

    public Quaternion GetItemRotation()
    {
        if (gridPlane == Grid.GridPlane.ZY)
        {
            return Quaternion.Euler(itemRotation + new Vector3(0, 90f, 0));
        }
        return Quaternion.Euler(itemRotation);
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

    public Inventory ReturnInventory()
    {
        return inventory;
    }
}