using UnityEngine;

public class Camerafollowmain : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform pickUpCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickUpCamera.position = mainCamera.position;
        pickUpCamera.rotation = mainCamera.rotation;
    }
}
