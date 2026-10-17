using UnityEngine;
using UnityEngine.Assertions.Must;

public class WandboxSelectionScript : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    //[SerializeField] private GameObject wandboxPrefab; //Don't think this is useful any longer. 
    [SerializeField] private Transform wandSpawnLocation;
    [SerializeField] private Transform wandLidSpawnLocation;
    [SerializeField] private float raycastRange = 100f;
    [SerializeField] private WorkStationSit workStationSit;

    private int count = 0;
    GameObject duplicatedBox;
    Transform lid;


    // Update is called once per frame
    void Update()
    {
        if (workStationSit.IsSitting() == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectWandBox();
            }
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
                Debug.Log("Ray hit: " + hit.collider.name);
                if (hit.collider.CompareTag("WandBox"))
                {
                    if (count == 0) //First box spawn
                    {
                        duplicatedBox = Instantiate(hit.collider.gameObject, wandSpawnLocation.position, wandSpawnLocation.rotation); //Click on the appropriate sized box -> duplicate box spawns infron of you.

                        duplicatedBox.tag = "canPickUp"; //So that wandbox can be picked up after item has been stored inside.
                        //Made the original item kinematic, removed need for next line.
                        //duplicatedBox.GetComponent<Rigidbody>().isKinematic = true;


                        //Creating and duplicating lid
                        lid = duplicatedBox.transform.Find("WandboxLid");

                        lid.position = wandLidSpawnLocation.position;
                        lid.rotation = wandLidSpawnLocation.rotation;
                        lid.tag = "WorkStationItem";
                        count += 1;
                    }
                    else if (count == 1)
                    {
                        Destroy(duplicatedBox);

                        duplicatedBox = Instantiate(hit.collider.gameObject, wandSpawnLocation.position, wandSpawnLocation.rotation); //Destroys previously summoned box and replaces it with the newly clicked box-type.

                        lid = duplicatedBox.transform.Find("WandboxLid");

                        lid.position = wandLidSpawnLocation.position;
                        lid.rotation = wandLidSpawnLocation.rotation;

                        lid.tag = "WorkStationItem"; //To enable item dragging FOR LID ONLY
                        //duplicatedBox.tag = "canPickUp";
                        //duplicatedBox.GetComponent<Rigidbody>().isKinematic = true;
                    }
                }
            }
        }
    }
}
