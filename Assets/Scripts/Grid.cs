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

        // Debug.Log(width + " " + height); Array working (verified)
        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) //This here cycles through the first dimension or x-cordinate or "collumns" of the array, here 0 means the first dimension and 1 means 2nd dimension
        {
            for (int y = 0; y < gridArray.GetLength(1); y++) //This one is height, x is width.
            {
                UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize,cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter); //"GetWorldPosition(x, y) + new Vector3(cellSize,cellSize)" controls where in a grid are objects/text shown
                //The following two lines are to help visualize what the grid looks like and are not necessary as our grid will be invisible in the final product.
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1),Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f); //Draw horizontal line from (0,maxHeight) - (maxWidth,maxHeight)
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f); //Same thing but vertical
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize; //I believe if we want to change where the grid is, for example the grid should span x-y or z-y, we change this here, as Vector3 has (x,y,z). Or we could try rotating through inspector
        //Also, we could probably add slanted shelves like in the real olivander's store with this with 1,1,1.
    }
}
