using UnityEngine;
using CodeMonkey.Utils;

public class TestingGrid : MonoBehaviour
{
    private Grid grid;

    void Awake() //So that grid is initialized before StoreInShelfScript everytime.
    {
        //Use this to change size, shape, position of grid on the world
        grid = new Grid(3, 5, 0.8f, new Vector3(-5,0)); //the third parameter here is cellSize, change it to change how many units apart each grid should be.
    }
    public Grid GetGrid()
    {
        return grid;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //If left click is pressed while mouse is over grid, change value of grid using "SetGridValue" method defined in Grid script.
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);
            grid.SetGridValue(UtilsClass.GetMouseWorldPosition(), 1);


        }

        if (Input.GetMouseButtonDown(1)) //Button to get the value stored inside a grid
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}