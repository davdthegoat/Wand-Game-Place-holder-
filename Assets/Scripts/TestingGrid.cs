using UnityEngine;
using CodeMonkey.Utils;

public class TestingGrid : MonoBehaviour
{
    private Grid grid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new Grid(10, 10, 10f); //the third parameter here is cellSize, change it to change how many units apart each grid should be.
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //If left click is pressed while mouse is over grid, change value of grid using "SetGridValue" method defined in Grid script.
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            Debug.Log(mouseWorldPosition);
            grid.SetGridValue(UtilsClass.GetMouseWorldPosition(), 56);
        }
    }
}