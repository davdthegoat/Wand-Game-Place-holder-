using UnityEngine;

public class WandboxSelectionScript : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject wandboxPrefab; //Later on, there will be multiple fields for each respective box size. //Maybe not.
    [SerializeField] private Transform wandSpawnLocation;
    [SerializeField] private float raycastRange = 100f;
    [SerializeField] private WorkStationSit workStationSit;

    private int count = 0;
    GameObject duplicatedBox;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectWandBox();
        } 
    }

    private void SelectWandBox() //Chose the correct sized box for your needs.
    //Reused code from previous scripts mostly.
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (workStationSit.IsSitting() == true)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, raycastRange))
            {
                if (hit.collider.CompareTag("WandBox"))
                {
                    if (count == 0) //First box spawn
                    {
                        duplicatedBox = Instantiate(hit.collider.gameObject, wandSpawnLocation.position, wandSpawnLocation.rotation); //Click on the appropriate sized box -> duplicate box spawns infron of you.

                        duplicatedBox.tag = "canPickUp"; //So that wandbox can be picked up after item has been stored inside.

                        count += 1;
                    }
                    else if (count == 1)
                    {
                        Destroy(duplicatedBox);
                        duplicatedBox = Instantiate(hit.collider.gameObject, wandSpawnLocation.position, wandSpawnLocation.rotation); //Destroys previously summoned box and replaces it with the newly clicked box-type.

                        duplicatedBox.tag = "canPickUp";
                    }
                }
            }
        }
    }
}
