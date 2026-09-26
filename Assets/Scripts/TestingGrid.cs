using UnityEngine;

public class TestingGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Grid grid = new Grid(10, 10, 10f); //the third parameter here is cellSize, change it to change how many units apart each grid should be.
    }
}