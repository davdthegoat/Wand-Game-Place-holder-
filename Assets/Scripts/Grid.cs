using Unity.VisualScripting;
using UnityEngine;
//using CodeMonkey.Utils; UNCOMMENT TO RESTORE GRID DEBUG FUNCTIONALITY

public class Grid
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;
    [SerializeField] Vector3 originPositionOfGrid;
    private int[,] gridArray; //Apparently this is how to declare a 2D array
    //private TextMesh[,] gridDebuggingArray; No longer need debug, grid works.

    public Grid(int width, int height, float cellSize, Vector3 originPositionOfGrid) //We might need an additional variable called depth depending on how we want to go about things
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPositionOfGrid = originPositionOfGrid;

        // Debug.Log(width + " " + height); Array working (verified)
        gridArray = new int[width, height];
        //gridDebuggingArray = new TextMesh[width, height]; No longer debugging

        for (int x = 0; x < gridArray.GetLength(0); x++) //This here cycles through the first dimension or x-cordinate or "collumns" of the array, here 0 means the first dimension and 1 means 2nd dimension
        {
            for (int y = 0; y < gridArray.GetLength(1); y++) //This one is height, x is width.
            {
                //UN-COMMENT NEXT LINE TO RESTORE GRID DEBUG.
                //gridDebuggingArray[x,y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPositionCenter(x, y), 10, Color.white, TextAnchor.MiddleCenter); //"GetWorldPosition(x, y) + new Vector3(cellSize,cellSize)" controls where in a grid are objects/text shown
                //The following two lines are to help visualize what the grid looks like and are not necessary as our grid will be invisible in the final product.
                //GetWorldPositionCenter(x,y) is meant to replace GetWorldPosition(x,y) + new Vector3(cellSize,cellSize)*0.5f
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1),Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f); //Draw horizontal line from (0,maxHeight) - (maxWidth,maxHeight)
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f); //Same thing but vertical

        //SetGridValue(2, 1, 56); //Cordinate, and value set within cordinate.
    }


    //Get's world position from grid position
    public Vector3 GetWorldPosition(int x, int y) //get's global position based on x,y cords of the grid.
    {
        return new Vector3(x, y) * cellSize + originPositionOfGrid; 
        //I believe if we want to change where the grid is, for example the grid should span x-y or z-y, we change this here, as Vector3 has (x,y,z). Or we could try rotating through inspector
        //Also, we could probably add slanted shelves like in the real olivander's store with this with 1,1,1.
        //"+ originPositionOfGrid" helps calculate wherer exactly the world position is on global coordinates.
        //"new Vector3 (cellSize, cellSize)*0.5f" adjusts coorindates to be in the exact center of the grid instead of a corner: Future DVD here, removed because gird-lines are drawn from this method. Offsetting it means offsetting the lines, which is worse for visual representation
    }

    public Vector3 GetWorldPositionCenter(int x, int y) //This returns the center of a grid
    {
        return GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
    }

    //Get's grid position from world position. 
    public void GetXYCoordinates(Vector3 worldPosition, out int x, out int y) //Get's the x,y cords based on world position.
    {
        x = Mathf.FloorToInt((worldPosition-originPositionOfGrid).x / cellSize); //Calculates in which grid does the current cordinate lay.
        y = Mathf.FloorToInt((worldPosition-originPositionOfGrid).y / cellSize);
    }

    public void GetGridCoords(Vector3 worldPosition, out int x, out int y)
    {
        GetXYCoordinates(worldPosition, out x, out y); //Although this is the same as the method above this provides controlled access to which scripts can and can't see this calculation (I think...)
    }
    public void SetGridValue(int x, int y, int targetValue)
    {
        if (x >= 0 && y >= 0 && x < width && y < height) //Will have to change for three dimensions. This is validating the values that the grid can take.
        {
            gridArray[x, y] = targetValue;
            //gridDebuggingArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetGridValue(Vector3 worldPosition, int value) //Set value to grid cords by itself instead of having to do it manually.
    {
        int x, y;
        GetXYCoordinates(worldPosition, out x, out y);
        SetGridValue(x, y, value);
    }

    public int GetValue(int x, int y) //Read value stored inside a grid
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition) //Read value stored inside a grid, the sequel
    {
        int x, y;
        GetXYCoordinates(worldPosition, out x, out y);
        return GetValue(x, y);
    }

}
