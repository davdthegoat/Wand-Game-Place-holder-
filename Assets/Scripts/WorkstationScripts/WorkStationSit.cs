using UnityEngine;

public class WorkStationSit : MonoBehaviour
{
    //This script is meant to seat Mr.Bean to the workstation. Depending on how things unfold, it might also be used for freezing the camera and letting go of Mr.Bean.

    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSitPosition;
    [SerializeField] private float interactRange = 3f;
    private Camera playerCamera;
    void Start()
    {
        playerCamera = Camera.main; //Makes the main camera linked with var playerCamera.
    }

    private void TrySit()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("WorkBench"))
            {
                player.transform.position = playerSitPosition.position;
                player.transform.rotation = playerSitPosition.rotation;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TrySit();
        }
    }
}
