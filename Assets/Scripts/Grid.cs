using Unity.VisualScripting;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;
    private int[,] gridArray; //Apparently this is how to declare a 2D array

    public Grid(int width, int height, float cellSize) //We might need an additional variable called depth depending on how we want to go about things
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) //This here cycles through the first dimension or x-cordinate or "collumns" of the array, here 0 means the first dimension and 1 means 2nd dimension
        {
            for (int y = 0; x < gridArray.GetLength(1); y++) 
            {
                UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y), 20, Color.white, TextAnchor.MiddleCenter);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize; //What is x - y here?
    }
}
