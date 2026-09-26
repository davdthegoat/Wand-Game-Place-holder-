using UnityEngine;

public class TestingGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Grid grid = new Grid(4, 2, 10f); //the third parameter here is cellSize
    }
}